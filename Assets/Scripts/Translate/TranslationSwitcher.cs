using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TranslationSwitcher : MonoBehaviour
{
    public TextMeshProUGUI myTMPro;
    public string myTranslation;

    public void SwitchTranslation()
    {
        SaveValues.instance.SetLanguage(myTranslation);
        Manager.Instance.closeTranslationPanel.OnClick();
    }
}
