using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

public static class TranslationManager
{
    private static Dictionary<string, string> translations = new();
    public static TMP_FontAsset font;
    public static void LoadTranslations(string fileName)
    {
        TextAsset txtFile = Resources.Load<TextAsset>(fileName);
        if (txtFile != null)
        {
            string[] lines = txtFile.text.Split('\n');
            foreach (string line in lines)
            {
                if (line.Trim().StartsWith("#"))
                {
                    continue; // Skip lines that start with #
                }

                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    translations[key] = value;
                }
            }
        }
    }


    public static string Translate(string key)
    {
        if (translations.TryGetValue(key, out string translation))
        {
            return translation;
        }
        else
        {
            return key; // Return the key itself if translation not found
        }
    }
}
