using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPack", menuName = "New Pack")]
public class PackSO : ScriptableObject
{
    public string nameKey;
    public string descKey;
    public List<PackItemSO> items;
    public GameObject inShopPrefab;
}