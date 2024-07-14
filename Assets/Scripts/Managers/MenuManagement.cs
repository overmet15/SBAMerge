using UnityEngine;

public class MenuManagement : MonoBehaviour
{
	public static MenuManagement instance;
	[SerializeField] private GameObject normalUI, fullscreenUI;
    public bool windowEnabled = false;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SettingsWindowManager.inited = true;
#if UNITY_ANDROID
        normalUI.SetActive(true);
#endif
    }

    public void ToggleFullscreenUI(bool fullscreen)
    {
#if !UNITY_ANDROID
        normalUI.SetActive(!fullscreen);
        fullscreenUI.SetActive(fullscreen);
#endif
    }
    public void ToggleFullscreen()
	{
		SaveValues.instance.ToggelFullscreen();
	}
	public void ShowLeaderboard() { Core.instance.ShowLeaderboard(); }
	public void ShowAchivements() { Core.instance.ShowTrophies();}
	public void ToggleControls()
	{
#if !UNITY_ANDROID
		if (SaveValues.instance.optionsData.keyboardControls)
		{
            SaveValues.instance.optionsData.keyboardControls = false;
		}
		else
		{
            SaveValues.instance.optionsData.keyboardControls = true;
		}
		foreach (TranslateChangeControls translate in FindObjectsOfType<TranslateChangeControls>()) translate.ChangeTranslation();
#endif
    }
    public void OpenDiscordLink()
    {
        Application.OpenURL("https://discord.com/invite/9fZdXA6c8n");
    }

}
