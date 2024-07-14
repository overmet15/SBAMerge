using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
public class SBAMergeTools : EditorWindow
{
    private GameObject FullscreenUI, NormalUI, FullscreenSettingsUI, NormalSettingsUI, shopUI;
    private bool fullscreenUIToggle, normalUIToggle, settingsToggle, shopUIToggle;

    [MenuItem("Tools/SBA Merge Tools")] public static void ShowWindow() { EditorWindow.GetWindow<SBAMergeTools>(); }

    private void OnGUI()
    {
        if (Application.isPlaying)
        {
            GUILayout.Label("Stats:");
            GUILayout.Label($"Balls on scene: {Manager.instance.allBalls.Count}");
            GUILayout.Label($"Game Version: {Application.version}");

            GUILayout.Space(5);
            if (!Core.instance.signedIn)
            {
                if (GUILayout.Button("Sign In To GameJolt"))
                {
                    GameJolt.API.GameJoltAPI.Instance.CurrentUser = new("overmet15", "MostSecuredTokenEver");
                    GameJolt.API.GameJoltAPI.Instance.CurrentUser.SignIn();
                }
            }
            else
            {
                if (GUILayout.Button("Sign Out From GameJolt"))
                {
                    GameJolt.API.GameJoltAPI.Instance.CurrentUser.SignOut();
                }
            }
        }
        else GUILayout.Label("Enter play mode to view stats and etc.");
        GUILayout.Space(5);
        if (GUILayout.Button("Init")) InitializeGameObjects();
        
        if (CheckForNullItems())
        {
            fullscreenUIToggle = EditorGUILayout.Toggle("Fullscreen UI", fullscreenUIToggle);
            FullscreenUI.SetActive(fullscreenUIToggle);

            normalUIToggle = EditorGUILayout.Toggle("Normal UI", normalUIToggle);
            NormalUI.SetActive(normalUIToggle);

            shopUIToggle = EditorGUILayout.Toggle("Shop UI", shopUIToggle);
            shopUI.SetActive(shopUIToggle);

            settingsToggle = EditorGUILayout.Toggle("Settings UI (Editor mode)", settingsToggle);
            FullscreenSettingsUI.SetActive(settingsToggle);
            NormalSettingsUI.SetActive(settingsToggle);
        }
    }
    bool CheckForNullItems()
    {
        if (FullscreenUI == null || NormalUI == null || FullscreenSettingsUI == null || NormalSettingsUI == null) 
        {
            return false; 
        }
        else return true;
    }
    private void InitializeGameObjects() // Stupid Chat GPT ahh code.
    {
        FullscreenUI = FindInactiveObjectByName("Fullscreen");
        NormalUI = FindInactiveObjectByName("Normal");
        FullscreenSettingsUI = FindInactiveObjectByName("SettingsStuff", "Fullscreen");
        NormalSettingsUI = FindInactiveObjectByName("SettingsStuff", "Normal");
        shopUI = FindInactiveObjectByName("ShopPanel");
        
        normalUIToggle = true;
    }
    private GameObject FindInactiveObjectByName(string name, string parentName = null)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name && (parentName == null || (obj.transform.parent != null && obj.transform.parent.name == parentName)))
            {
                return obj;
            }
        }
        return null;
    }
}
#endif