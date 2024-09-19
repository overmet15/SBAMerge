using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

public static class TranslationManager
{
    private static Dictionary<string, string> translations = new();
    public static TMP_FontAsset font;
    public static void LoadTranslations(string trantlationFile)
    {
        string txtFile = trantlationFile;
        if (txtFile != null)
        {
            string[] lines = txtFile.Split('\n');
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

    public static string TranslateFromFile(string list, string key)
    {
        Dictionary<string, string> tr = new();
        string txtFile = list;
        if (txtFile != null)
        {
            string[] lines = txtFile.Split('\n');
            foreach (string line in lines)
            {
                if (line.Trim().StartsWith("#"))
                {
                    continue; // Skip lines that start with #
                }

                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string key2 = parts[0].Trim();
                    string value = parts[1].Trim();
                    tr[key2] = value;
                }
            }
        }
        return tr[key];
    }
}
