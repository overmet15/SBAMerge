using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTipChangeControls : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject tip;
    [SerializeField] private TextMeshProUGUI tipText;
    private void Start()
    {
        tip.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tip.SetActive(false);
    }
    public void ChangeTranslation()
    {
        tipText.text = string.Concat(str2: (!SaveValues.instance.optionsData.keyboardControls) ? TranslationManager.Translate("Mouse") : TranslationManager.Translate("Keyboard"), str0: TranslationManager.Translate("ChangeControls"), str1: ": \n");
        tipText.font = TranslationManager.font;
    }
}
