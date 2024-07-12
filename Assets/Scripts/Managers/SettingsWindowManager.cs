using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindowManager : MonoBehaviour //Move this shit to UI folder
{

    [SerializeField] private GameObject obj;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject touchPanel;
    [SerializeField] private GameObject toggleFullscreenButton, keyboardToggle;
    [SerializeField] private Toggle indicatorToggle;

    public static bool inited;
    private void Start()
    {
        indicatorToggle.Set(SaveValues.instance.optionsData.loadingIndicatorNeeded);
        indicatorToggle.OnValueChanged.AddListener(b => ToggleIndicator(b));
#if UNITY_ANDROID
        SaveValues.instance.optionsData.keyboardControls = false;
        toggleFullscreenButton.SetActive(false);
        keyboardToggle.SetActive(false);
#endif 
    }
    void OnEnable()
    {
        if (!inited) return;
        musicSlider.value = SaveValues.instance.optionsData.musicVolume;
        sfxSlider.value = SaveValues.instance.optionsData.sfxVolume;
        indicatorToggle.Set(SaveValues.instance.optionsData.loadingIndicatorNeeded);
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
        touchPanel.SetActive(!SaveValues.instance.optionsData.keyboardControls);
    }
    public void OnSettingsButton()
    {
        MenuManagement.instance.windowEnabled = !MenuManagement.instance.windowEnabled;
    }
    public void ToggleIndicator(bool toggle)
    {
        SaveValues.instance.optionsData.loadingIndicatorNeeded = toggle;
    }
}
