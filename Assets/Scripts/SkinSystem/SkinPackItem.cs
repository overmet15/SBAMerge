using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SkinPackItem : MonoBehaviour, IPointerDownHandler
{
    public SkinPack skinPack;

    [SerializeField] private Translate nameTextTranslate;
    [SerializeField] private Translate descTextTranslate;

    private ShopManager shopManager;

    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
        nameTextTranslate.key = skinPack.skinpackNameKey;
        descTextTranslate.key = skinPack.skinpackDescKey;
        nameTextTranslate.ChangeTranslation();
        descTextTranslate.ChangeTranslation();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        shopManager.CreateSkinListWindow(skinPack);
    }
}
