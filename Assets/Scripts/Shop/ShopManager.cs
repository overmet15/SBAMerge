using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public enum ShopPanels { buff, almanac, skinBuy}
    [SerializeField] private GameObject buffPanel;
    [SerializeField] private GameObject almanacPanel;
    [SerializeField] private GameObject skinBuyPanel;

    [SerializeField] private GameObject almanacSkinCreationPoint;
    private void Start()
    {
        foreach (SkinPack pack in SkinManager.skinPacks.Values)
        {
            CreateSkinItem(pack.skinpackPath, pack);
        }
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
        if (SaveValues.instance.gameData.coinCount > cost-1)
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
            SaveValues.instance.SaveGDataAsync();
        }
        else
        {
            // showErrorMessage("ERR", "not enough money");
        }
    }

    public void CreateSkinItem(string packPath, SkinPack skinPack)
    {
        Instantiate(Resources.Load<GameObject>($"Skins/{packPath}/SkinPackItem"), almanacSkinCreationPoint.transform).GetComponent<SkinPackItem>().skinPack = skinPack;
    }

    public void CreateSkinListWindow(SkinPack skinPack)
    {

    }
}
