using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class SaveData
{
    private static string path = "/options.json";

	private static string savePath = "/data.json";

	private static string debugSavePath = "/DEBUG_data.json";

    private static readonly int currentSaveFileVersion = 2;
    public static void Init()
    {
		path = Application.persistentDataPath + path;
		savePath = Application.persistentDataPath + savePath;
        debugSavePath = Application.persistentDataPath + debugSavePath;
        if (File.Exists(Application.persistentDataPath + "/console.log"))
        {
            FileInfo fileInfo = new(Application.persistentDataPath + "/console.log");
            long fileSizeInBytes = fileInfo.Length;
            long fileSizeInMB = fileSizeInBytes / (1024 * 1024);
            if (fileSizeInMB > 8) { File.Delete(Application.persistentDataPath + "/console.log"); }
        }
	}


    public static async Task SaveGameDataAsync(GameData saveData, bool showLoading = true)
    {
        Core.instance.ToggleLoadingIndicator(showLoading);
        try
        {
            //saveData.skinUnlockedValues.Clear();
            //foreach (var d in SaveValues.instance.postSkinData)
            //{
            //    SkinUnlockedValue val = new() { skinName = d.Key, skinUnlocked = d.Value };
            //    saveData.skinUnlockedValues.Add(val);
            //}
            string contents = JsonUtility.ToJson(saveData);
            await Task.Run(() => JsonEncryptHelper.SaveEncryptedJson(savePath, contents));
            string[] a = Environment.GetCommandLineArgs();
            if (a.Contains("-debug") || Application.isEditor)
            {
                await Task.Run(() => File.WriteAllText(debugSavePath, contents));
            }
            Core.instance.ToggleLoadingIndicator(false);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save game data: {ex.Message}");
            Core.instance.ToggleLoadingIndicator(false);
        }
    }

    public static void LogMessage(string logString, string stackTrace, LogType type)
    {
        string logEntry = "{0}\n{1}\n{2}\n{3}\n{0}\n\n"; // Type, Time, Log, stackTrace
        string s0 = "";

        if (type == LogType.Error) s0 = "! ! ! ! !";
        else if (type == LogType.Log) s0 = "= = = = =";
        else if (type == LogType.Warning) s0 = "? ? ? ? ?";
        else if (type == LogType.Exception) s0 = "!? !? !? !? !?";

        logEntry = string.Format(logEntry, s0, DateTime.Now.ToString(), logString, stackTrace);

        File.AppendAllText(Application.persistentDataPath + "/console.log", logEntry);
    }

    public static GameData LoadGameData()
	{
        if (File.Exists(savePath)) return JsonUtility.FromJson<GameData>(JsonEncryptHelper.LoadEncryptedJson(savePath));
        else return new()
        {
            bombBuffCount = 3,
            shakeBuffCount = 3,
            slowMoBuffCount = 3,
            coinCount = 100,
            saveFileVersion = currentSaveFileVersion
        };
	}

	public static void SaveOptionsJson()
	{
		string contents = JsonUtility.ToJson(SaveValues.instance.optionsData);
		File.WriteAllText(path, contents);
	}

	public static OptionsData LoadOptionsJson()
	{
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			OptionsData currentOptionsData = JsonUtility.FromJson<OptionsData>(json);
			return currentOptionsData;
		}
		else
		{
            OptionsData optionsData = new()
            {
            musicVolume = 100f,
            sfxVolume = 100f,
            language = CheckSystemLanguage(),
            keyboardControls = false,
            loadingIndicatorNeeded = true
            };
            return optionsData;
        }
	}
    private static string CheckSystemLanguage()
    {
        if (Application.systemLanguage == SystemLanguage.English) return "English";
        else if (Application.systemLanguage == SystemLanguage.Russian) return "Russian";
        else if (Application.systemLanguage == SystemLanguage.French) return "French";
        else return "English";
    }
}

[System.Serializable]
public class GameData
{
    public int saveFileVersion;

	public int mainModeScore;
	public int inGameScore;
	public int coinCount;
	public int bombBuffCount, shakeBuffCount, slowMoBuffCount;
    public int MostMergedBalls;
    #if Development
    public List<SkinUnlockedValue> skinUnlockedValues;
#endif
}

[System.Serializable]
public class OptionsData
{
	public float musicVolume, sfxVolume;
	public string language;
	public bool keyboardControls;
    public bool loadingIndicatorNeeded;
    public int FPS;
}
#if Development

[System.Serializable]
public class SkinUnlockedValue
{
    public string skinName;
    public bool skinUnlocked;
}
#endif
public static class JsonEncryptHelper // Stupid Chat GPT-ahh class
{
    private static readonly byte[] key = Encoding.UTF8.GetBytes("totalysecuredbyteeversogetrealha");
    private static readonly byte[] iv = Encoding.UTF8.GetBytes("weirdesteverbruh");

    public static void SaveEncryptedJson(string filePath, string jsonData)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        // Create an encryptor to perform the stream transform.
        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using MemoryStream msEncrypt = new();
        using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using StreamWriter swEncrypt = new(csEncrypt);
            //Write all data to the stream.
            swEncrypt.Write(jsonData);
        }
        File.WriteAllBytes(filePath, msEncrypt.ToArray());
    }
    public static string LoadEncryptedJson(string filePath)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        // Create a decryptor to perform the stream transform.
        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for decryption.
        using MemoryStream msDecrypt = new(File.ReadAllBytes(filePath));
        using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
        using StreamReader srDecrypt = new(csDecrypt);
        // Read the decrypted bytes from the decrypting stream and place them in a string.
        return srDecrypt.ReadToEnd();
    }
}
