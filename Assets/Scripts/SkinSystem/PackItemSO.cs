using UnityEngine;
[CreateAssetMenu(fileName = "NewPackItem", menuName = "New Pack Item")]
public class PackItemSO : ScriptableObject
{
    public PackItemType packItemType;
    public SkinCharacter skinCharacter;
    public Rarity rarity;

    public string nameKey;
    public string descKey;
    //public int cost;
    public bool unlocked = false;
    public string saveKey;

    public Sprite sprite;
    public Color color = Color.white;
}