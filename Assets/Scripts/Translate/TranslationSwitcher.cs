using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TranslationSwitcher : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private TMP_FontAsset font;

    public void SwitchTranslation()
    {
        TranslationManager.font = font;
        SaveValues.instance.HandleTranslation(id);
    }
}
