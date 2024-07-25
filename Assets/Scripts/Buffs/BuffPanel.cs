using UnityEngine;
using UnityEngine.UI;

public class BuffPanel : MonoBehaviour
{
    [SerializeField]
    private Slider freezeSlider;

    [SerializeField]
    private Slider cooldownSlider;

    void Update()
    {
        freezeSlider.maxValue = BuffManager.instance.slowMoCooldownMax;
        cooldownSlider.maxValue = BuffManager.instance.cooldownMax;
        freezeSlider.value = BuffManager.instance.slowMoCooldown;
        cooldownSlider.value = BuffManager.instance.cooldown;
    }
}
