using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {  get; private set; }

    [SerializeField] private GameObject purchaseSound;
    [SerializeField] private GameObject notifySound;
    void Awake()
    {
        instance = this;
    }

    public void PurchaseSound()
    {
        Instantiate(purchaseSound);
    }

    public void NotifySound()
    {
        Instantiate(notifySound);
    }
}
