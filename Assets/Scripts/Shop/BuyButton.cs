using UnityEngine;

public class BuyButton : MonoBehaviour
{
	[SerializeField] private BuffManager.buffType type;
	[SerializeField] private int cost;
    [SerializeField] private ShopManager shopManager;

    public void OnButton()
	{
        shopManager.CreateBuffBuyingWindow(type, cost);
	}
}
