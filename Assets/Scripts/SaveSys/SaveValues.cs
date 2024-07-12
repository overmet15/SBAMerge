using GameJolt.API;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SaveValues : MonoBehaviour // This script is such a mess right now, redo it soon
{
	public static SaveValues instance;

	public GameData gameData;
	public OptionsData optionsData;
    public Dictionary<string, bool> postSkinData = new();

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11)) ToggelFullscreen();
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
        foreach (var k in gameData.skinUnlockedValues)
        {
            postSkinData.Add(k.skinName, k.skinUnlocked);
        }
        //SkinManager.Init();
        HandleTranslation(optionsData.language);
    }
    public async void SaveGDataAsync()
    {
        await SaveData.SaveGameDataAsync(gameData);
    }

	private GameData CreateDefaultValues()
	{
		GameData gameData = new()
        {
            bombBuffCount = 3,
            shakeBuffCount = 3,
            slowMoBuffCount = 3,
            coinCount = 100
        };
		return gameData;
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
        foreach (Translate translate in FindObjectsOfType<Translate>())
        {
            translate.ChangeTranslation();
        }
#if !UNITY_ANDROID
		foreach (TranslateChangeControls translate in FindObjectsOfType<TranslateChangeControls>()) translate.ChangeTranslation();
#endif
        optionsData.language = id;
    }

    public void ToggelFullscreen()
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
        SaveData.SaveOptionsJson();
    }
}
