using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public enum ShopPanels { buff, almanac, skinBuy}
    [SerializeField] private GameObject buffPanel;
    [SerializeField] private GameObject almanacPanel;
    [SerializeField] private GameObject skinBuyPanel;
    public GameObject canvas;

    [SerializeField] private GameObject almanacSkinCreationPoint;
    public List<PackSO> packs = new();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
#if Development
        foreach (PackSO pack in packs)
        {
            Instantiate(pack.inShopPrefab, almanacPanel.transform.GetChild(0)).GetComponent<SkinPackItem>().Init(pack);
        }
#endif
    }
    public void TogglePanels(ShopPanels panel)
    {
        if (panel == ShopPanels.buff)
        {
            buffPanel.SetActive(true);
            almanacPanel.SetActive(false);
            skinBuyPanel.SetActive(false);
        }
        else if (panel == ShopPanels.almanac)
        {
            buffPanel.SetActive(false);
            almanacPanel.SetActive(true);
            skinBuyPanel.SetActive(false);
        }
        else if (panel == ShopPanels.skinBuy)
        {
            buffPanel.SetActive(false);
            almanacPanel.SetActive(false);
            skinBuyPanel.SetActive(true);
        }
    }
    public void CreateBuffBuyingWindow(BuffManager.buffType type, int cost)
    {
        if (SaveValues.instance.gameData.coinCount >= cost)
        {
            switch (type)
            {
                case BuffManager.buffType.none:
                    //showErrorMessage("ERR", "Unable to buy null buff");
                    break;
                case BuffManager.buffType.bomb:
                    SaveValues.instance.gameData.bombBuffCount++;
                    break;
                case BuffManager.buffType.timeFreeze:
                    SaveValues.instance.gameData.slowMoBuffCount++;
                    break;
                case BuffManager.buffType.shake:
                    SaveValues.instance.gameData.shakeBuffCount++;
                    break;
            }
            SaveValues.instance.gameData.coinCount -= cost;
        }
        else
        {
            // showErrorMessage("ERR", "not enough money");
        }
    }
}
