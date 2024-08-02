using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkinPackItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Translate nameTextTranslate;
    [SerializeField] private Translate descTextTranslate;
    private PackSO mySO;
    [SerializeField] private GameObject displayPrefab;
    private GameObject hasDisplay;

    public void Init(PackSO so)
    {
        nameTextTranslate.key = so.nameKey;
        descTextTranslate.key = so.descKey;
        mySO = so;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (hasDisplay == null) hasDisplay = Instantiate(displayPrefab, ShopManager.instance.canvas.transform);
        hasDisplay?.GetComponent<PackItemList>().Init(mySO);
        Debug.Log("A");
    }
}
