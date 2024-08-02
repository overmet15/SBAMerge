using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SaveValues : MonoBehaviour // This script is such a mess right now, redo it soon
{
	public static SaveValues instance;

	public GameData gameData;
	public OptionsData optionsData;

    [SerializeField] private TMPro.TMP_FontAsset[] fontAssetsPreload;

    private Vector2Int rememberedResolution;
    private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			SaveData.Init();
            Load();
        }
		else Destroy(this);
	}

	private void Start()
    {
        rememberedResolution = new Vector2Int(600, 800);
        StartCoroutine(SaveEverySec());
    }

    public float ReturnSoundValue(bool IsMusic)
    {
        if (IsMusic)
        {
            return optionsData.musicVolume / 100f;
        }
        return optionsData.sfxVolume / 100f;
    }
    void Load()
    {
        gameData = SaveData.LoadGameData();

        optionsData = SaveData.LoadOptionsJson();

        if (gameData.skinUnlockedValues != null)
        //SkinManager.Init();
        HandleTranslation(optionsData.language);
    }
    public async Task SaveGDataAsync(bool showLoading = true)
    {
        await SaveData.SaveGameDataAsync(gameData, showLoading);
    }
    public async void SaveGData(bool showLoading = true)
    {
        await SaveData.SaveGameDataAsync(gameData, showLoading);
    }

    public void HandleTranslation(int id)
    {
        string text = string.Empty;
        switch (id)
        {
            case 0:
                text = "en";
                break;
            case 1:
                text = "ru";
                break;
            case 2:
                text = "fr";
                break;
        }
        TranslationManager.font = fontAssetsPreload[id];
        TranslationManager.LoadTranslations("Translations/" + text);
        foreach (Translate translate in FindObjectsByType<Translate>(FindObjectsSortMode.None))
        {
            translate.ChangeTranslation();
        }
#if !UNITY_ANDROID
		foreach (TranslateChangeControls translate in FindObjectsByType<TranslateChangeControls>(FindObjectsSortMode.None)) translate.ChangeTranslation();
#endif
        optionsData.language = id;
    }

    public void ToggleFullscreen()
    {
#if !UNITY_ANDROID
        if (!Screen.fullScreen)
		{
			rememberedResolution = new Vector2Int(Screen.width, Screen.height);
			Screen.SetResolution(1920, 1080, true);
            MenuManagement.instance.ToggleFullscreenUI(true);
		}
		else 
        {
        Screen.SetResolution(rememberedResolution.x, rememberedResolution.y, false);
        MenuManagement.instance.ToggleFullscreenUI(false);
        }
#endif
    }

    IEnumerator SaveEverySec()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(SaveEverySec());
        gameData.inGameScore = Manager.instance.score;
        SaveData.SaveOptionsJson();
        SaveGData(false);
    }
}
