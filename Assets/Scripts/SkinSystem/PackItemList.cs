using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if Development
public class PackItemList : MonoBehaviour
{
    public GameObject itemListObject;
    public Transform itemListSpawn;
    public void Init(PackSO so)
    {
        foreach (PackItemSO item in so.items)
        {
            PackItemListItem obj = Instantiate(itemListObject, itemListSpawn).GetComponent<PackItemListItem>();

            obj.Init(item);
        }
    }
}
#endif