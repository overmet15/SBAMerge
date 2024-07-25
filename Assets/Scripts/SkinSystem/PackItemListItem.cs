using UnityEngine;
using UnityEngine.UI;

public class PackItemListItem : MonoBehaviour
{
    public PackItemSO mySO;
    public Image bg;
    public Image front;

    public void Init(PackItemSO so)
    {
        mySO = so;
        bg.color = so.color;
        front.sprite = so.sprite;
    }
}