using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tip;
    public TextMeshProUGUI tipText;
    public string tipTextTranslationKey;
    void Start()
    {
        tip.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tip.SetActive(true);
        tipText.text = TranslationManager.Translate(tipTextTranslationKey); 
        tipText.font = TranslationManager.font;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tip.SetActive(false);
    }
}
