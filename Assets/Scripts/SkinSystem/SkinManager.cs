using System.Collections.Generic;
using UnityEngine;

public static class SkinManager
{
    public enum SkinCharacter { shicka, baaren, turtleGolem, yeti, guardian, beeQueen, tristopio, brother, testBear}
    public enum SkinRarity {common, rare, epic, mythic, legendary}
    public static Dictionary<string, SkinPack> skinPacks = new();

    public static void Init()
    {
        // Lil tutorial, name and description must be keys from translation system lol;
        skinPacks.Clear();

        CreateSkinPack("test", "test pack", "TestPack");
        skinPacks.TryGetValue("test", out SkinPack testPack);
        testPack.AddSkin("testShickaBruh", SkinCharacter.shicka, SkinRarity.common, "Shicka");
    }

    private static void CreateSkinPack(string packNameKey, string skinDescriptionKey, string path) 
    {
        SkinPack skin = new() { skinpackNameKey = packNameKey, skinpackDescKey = skinDescriptionKey, skinpackPath = path };
        skinPacks.Add(packNameKey, skin);
    }
}

public class SkinPack
{
    public string skinpackNameKey;
    public string skinpackDescKey;
    public string skinpackPath;
    public Dictionary<string, Skin> skins = new();

    public void AddSkin(string newSkinName, SkinManager.SkinCharacter newSkinCharacter, SkinManager.SkinRarity newSkinRarity, string newSkinSpriteName)
    {
        Skin s = new(){ skinName = newSkinName, skinCharacter = newSkinCharacter,skinRarity = newSkinRarity, 
            skinSprite = Resources.Load<Sprite>($"Skins/{skinpackPath}/Sprites/{newSkinSpriteName}")
        };
        if (SaveValues.instance.postSkinData.ContainsKey(newSkinName))
        {
            SaveValues.instance.postSkinData.TryGetValue(newSkinName, out bool b);
            s.unlocked = b;
        }
        else
        {
            s.unlocked = false;
            SaveValues.instance.postSkinData.Add(newSkinName, false);
        }
        Debug.Log(s.unlocked);
        skins.Add(newSkinName, s);
    }
}
public class Skin
{
    public string skinName;
    public SkinManager.SkinCharacter skinCharacter;
    public SkinManager.SkinRarity skinRarity;
    public Sprite skinSprite;
    public bool unlocked;
}
