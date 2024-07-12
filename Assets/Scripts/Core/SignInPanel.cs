using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GameJolt.API;

public class SignInPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField tokenField;
    [SerializeField] private UnityEngine.UI.Toggle rememberMeToggle;
    [SerializeField] private UnityEngine.UI.Toggle showTokenToggle;
    [SerializeField] private TextMeshProUGUI errorText;
    void Start()
    {
        showTokenToggle.isOn = false;
        ToggleTokenShow(showTokenToggle.isOn);
        showTokenToggle.onValueChanged.AddListener(call => ToggleTokenShow(call));
        string username, token = "";
        rememberMeToggle.isOn = GameJoltAPI.Instance.GetStoredUserCredentials(out username, out token);
        usernameField.text = username;
        tokenField.text = token;
        errorText.text = string.Empty;
    }
    public void ToggleWindow()
    {
        GetComponent<Animator>().SetTrigger("Go");
    }

    public void Submit()
    {
        if (!Core.instance.signedIn)
        {
            if (usernameField.text.Trim() != string.Empty || tokenField.text.Trim() != string.Empty)
            {
                Core.instance.ToggleLoadingIndicator(true);
                group.interactable = false;
                var user = new GameJolt.API.Objects.User(usernameField.text.Trim(), tokenField.text.Trim());
                user.SignIn(signed => {
                    Core.instance.ToggleLoadingIndicator(false);
                    group.interactable = true;
                    if (signed)
                    {
                        ToggleWindow();
                        Core.instance.OnSignIn();
                    }
                    else
                    {
                        errorText.text = TranslationManager.Translate("AnErrorOccured");
                    }
                    }, 
                    userFetchedSuccess =>
                    {
                        
                    }, rememberMeToggle.isOn);
            }
            else errorText.text = TranslationManager.Translate("PleaseFillAllFields");
        }
        else Debug.LogWarning("Signed In Already");
    }
    void ToggleTokenShow(bool show) { tokenField.contentType = show ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password; tokenField.ActivateInputField(); }
    public void OpenLink() { Application.OpenURL("https://gamejolt.com/"); }
    public void OpenTutorial() { Application.OpenURL("https://youtu.be/VIujXYfOXMM"); }
}
