using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelTogle : MonoBehaviour
{
    [SerializeField] private ShopManager.ShopPanels panel;
    [SerializeField] private ShopManager shopManager;
    public void OnClick()
    {
        shopManager.TogglePanels(panel);
    }
}
