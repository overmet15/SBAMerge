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
#if UNITY_ANDROID
        normalUI.SetActive(true);
        fullscreenUI.SetActive(false);
#endif
#if UNITY_STANDALONE
        normalUI.SetActive(!Screen.fullScreen);
        fullscreenUI.SetActive(Screen.fullScreen);
#endif
    }

    public void ToggleFullscreenUI(bool fullscreen)
    {
#if UNITY_STANDALONE
        normalUI.SetActive(!fullscreen);
        fullscreenUI.SetActive(fullscreen);
#endif
    }
    public void ToggleFullscreen()
	{
		SaveValues.instance.ToggleFullscreen();
	}
	public void ShowLeaderboard() { Core.instance.ShowLeaderboard(); }
	public void ShowAchivements() { Core.instance.ShowTrophies();}
	public void ToggleControls()
	{
#if UNITY_STANDALONE
		if (SaveValues.instance.optionsData.keyboardControls)
		{
            SaveValues.instance.optionsData.keyboardControls = false;
		}
		else
		{
            SaveValues.instance.optionsData.keyboardControls = true;
		}
		foreach (TranslateChangeControls translate in FindObjectsByType<TranslateChangeControls>(FindObjectsSortMode.None)) translate.ChangeTranslation();
#endif
    }
}
