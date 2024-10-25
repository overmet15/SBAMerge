using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindowManager : MonoBehaviour //Move this shit to UI folder
{
    public static SettingsWindowManager Instance;
    [SerializeField] private GameObject obj;
    [SerializeField] private Slider musicSlider, sfxSlider, FPSSlider;
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private GameObject toggleFullscreenButton, keyboardToggle;
    [SerializeField] private Toggle indicatorToggle;
    [SerializeField] private GameObject buttonL, buttonR;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        indicatorToggle.Set(SaveValues.instance.optionsData.loadingIndicatorNeeded);
        indicatorToggle.OnValueChanged.AddListener(b => ToggleIndicator(b));
        musicSlider.value = SaveValues.instance.optionsData.musicVolume;
        sfxSlider.value = SaveValues.instance.optionsData.sfxVolume;
        indicatorToggle.Set(SaveValues.instance.optionsData.loadingIndicatorNeeded);
        SetUpFPSSlider();
        //OnFullscreenChange();
#if UNITY_ANDROID
        SaveValues.instance.optionsData.keyboardControls = false;
        toggleFullscreenButton.SetActive(false);
        keyboardToggle.SetActive(false);
#endif
    }
    void Update()
    {
        obj.SetActive(MenuManagement.instance.windowEnabled);
        SaveValues.instance.optionsData.musicVolume = musicSlider.value;
        SaveValues.instance.optionsData.sfxVolume = sfxSlider.value;
        FPSSLiderToSettings();
    }
    public void OnSettingsButton()
    {
        MenuManagement.instance.windowEnabled = !MenuManagement.instance.windowEnabled;
    }
    public void ToggleIndicator(bool toggle)
    {
        SaveValues.instance.optionsData.loadingIndicatorNeeded = toggle;
    }

    void SetUpFPSSlider()
    {
        int forSlider = SaveValues.instance.optionsData.FPS switch
        {
            15 => 0,
            30 => 1,
            60 => 2,
            120 => 3,
            _ => 3
        };

        FPSSlider.value = forSlider;
    }

    void FPSSLiderToSettings()
    {
        SaveValues.instance.optionsData.FPS = FPSSlider.value switch
        {
            0 => 15,
            1 => 30,
            2 => 60,
            3 => 120,
            _ => 30
        };

        fpsText.text = $"FPS: {SaveValues.instance.optionsData.FPS}";
    }

    public void OnFullscreenChange()
    {
#if UNITY_STANDALONE
        buttonL.SetActive(!Screen.fullScreen);
        buttonR.SetActive(Screen.fullScreen);
#endif
    }
}
