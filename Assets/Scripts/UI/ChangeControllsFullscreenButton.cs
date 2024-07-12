using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeControllsFullscreenButton : MonoBehaviour
{
    [SerializeField] private Sprite clickImage;
    [SerializeField] private Sprite keyboardImage;
    [SerializeField] private Image img;
    [SerializeField] private ButtonTipChangeControls b;
    private void Start()
    {
        if (SaveValues.instance.optionsData.keyboardControls) img.sprite = keyboardImage;
        else img.sprite = clickImage;
        b.ChangeTranslation();
    }
    public void OnClick()
    {
        SaveValues.instance.optionsData.keyboardControls = !SaveValues.instance.optionsData.keyboardControls;
        Start();
        b.ChangeTranslation();
    }
}
