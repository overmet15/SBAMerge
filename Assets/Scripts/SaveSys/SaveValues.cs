using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TMPro;

public class SaveValues : MonoBehaviour // This script is such a mess right now, redo it soon
{
	public static SaveValues instance;

	public GameData gameData;
	public OptionsData optionsData;

    [SerializeField] private TMPro.TMP_FontAsset[] fontAssetsPreload;
    public Dictionary<string, string> translations = new();

    public static Vector2Int rememberedResolution
    {
        get 
        { 
            return new Vector2Int
            (PlayerPrefs.GetInt("RememberedScreenResolutionX", 600), PlayerPrefs.GetInt("RememberedScreenResolutionY", 800));
        }
        private set 
        { 
            PlayerPrefs.SetInt("RememberedScreenResolutionX", value.x);
            PlayerPrefs.SetInt("RememberedScreenResolutionY", value.y);
        }
    }
    private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			SaveData.Init();
            Application.logMessageReceived += SaveData.LogMessage;
            Load();
        }
		else Destroy(this);
	}

	private void Start()
    {
        StartCoroutine(SaveEverySec());
    }

    void Update()
    {
        if (optionsData.FPS < 15) optionsData.FPS = 15;

        Application.targetFrameRate = optionsData.FPS;
    }
    public float ReturnSoundValue(bool IsMusic)
    {
        if (IsMusic) return optionsData.musicVolume / 100f;
        return optionsData.sfxVolume / 100f;
    }
    void Load()
    {
        gameData = SaveData.LoadGameData();

        optionsData = SaveData.LoadOptionsJson();

        //if (gameData.skinUnlockedValues != null)
        //SkinManager.Init();
        HandleTranslations();
    }
    public async Task SaveGDataAsync(bool showLoading = true)
    {
        await SaveData.SaveGameDataAsync(gameData, showLoading);
    }
    public async void SaveGData(bool showLoading = true)
    {
        await SaveData.SaveGameDataAsync(gameData, showLoading);
    }

    public void HandleTranslations()
    {
        TextAsset[] translations = Resources.LoadAll<TextAsset>("Translations");
        TranslationManager.font = fontAssetsPreload[0]; // -- en, ru, other
        foreach (TextAsset translation in translations)
        {
            this.translations.Add(translation.name, translation.text);
        }

        if (this.translations.ContainsKey(optionsData.language))
        {
            TranslationManager.LoadTranslations(this.translations[optionsData.language]);
            TranslationManager.font = GetFontForLanguage(optionsData.language);
        }
        else TranslationManager.LoadTranslations(this.translations["English"]);

        foreach (Translate translate in FindObjectsByType<Translate>(FindObjectsSortMode.None))
        {
            translate.ChangeTranslation();
        }
#if UNITY_STANDALONE
		foreach (TranslateChangeControls translate in FindObjectsByType<TranslateChangeControls>(FindObjectsSortMode.None)) translate.ChangeTranslation();
#endif
    }

    public void SetLanguage(string language)
    {
        optionsData.language = language; 
        TranslationManager.LoadTranslations(this.translations[language]);
        TranslationManager.font = GetFontForLanguage(language);
        foreach (Translate translate in FindObjectsByType<Translate>(FindObjectsSortMode.None))
            translate.ChangeTranslation();
    }

    public TMP_FontAsset GetFontForLanguage(string language)
    {
        return language switch
        {
            "English" => fontAssetsPreload[0],
            "Russian" => fontAssetsPreload[1],
            "French" => fontAssetsPreload[0],
            "German" => fontAssetsPreload[0],
            _ => fontAssetsPreload[0],
        };
    }
    public void ToggleFullscreen()
    {
#if UNITY_STANDALONE
        if (!Screen.fullScreen)
		{
			rememberedResolution = new Vector2Int(Screen.width, Screen.height);
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, true);
            MenuManagement.instance.ToggleFullscreenUI(true);
		}
		else 
        {
            Screen.SetResolution(rememberedResolution.x, rememberedResolution.y, false);
            MenuManagement.instance.ToggleFullscreenUI(false);
        }
        if (SettingsWindowManager.Instance != null)
            SettingsWindowManager.Instance.OnFullscreenChange();
#endif
    }

    IEnumerator SaveEverySec()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(SaveEverySec());
        gameData.inGameScore = Manager.Instance.score;
        SaveData.SaveOptionsJson();
        SaveGData(false);
    }
}
