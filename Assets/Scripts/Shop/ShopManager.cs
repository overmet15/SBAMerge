using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void CreateBuffBuyingWindow(BuffManager.buffType type, int cost)
    {
        if (SaveValues.instance.gameData.coinCount >= cost) return;
        switch (type)
        {
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
        SoundManager.instance.PurchaseSound();
    }
}
