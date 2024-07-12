using GameJolt.API;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutologinNotifier : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI autologinNotifyText;
    [SerializeField] private Animator autologinNotifyAnimator;
    [SerializeField] private Translate autologinNotifyTranslate;
    private string autologinColorString;
    private string autologinTextString;

    public void OnAutologin(AutoLoginResult result)
    {
        if (result == AutoLoginResult.Success)
        {
            ChangeNameColor();
            autologinNotifyAnimator.SetTrigger("Go");
            autologinNotifyText.text = autologinTextString + $" <color={autologinColorString}>" + GameJoltAPI.Instance.CurrentUser.Name + "</color>";
        }
        else if (result == AutoLoginResult.Failed)
        {
            // Unemplimented (ToDo)
        }
    }
    private void Update() { autologinTextString = autologinNotifyTranslate.output; }
    public void ChangeNameColor()
    {
        if (GameJoltAPI.Instance.CurrentUser.Name == "overmet15") autologinColorString = ColorToHex(Color.green);
        else if (GameJoltAPI.Instance.CurrentUser.Name == "SashaDandy") autologinColorString = ColorToHex(Color.red);
        else if (GameJoltAPI.Instance.CurrentUser.Name == "BearsBoi") autologinColorString = ColorToHex(Color.magenta);
        else autologinColorString = ColorToHex(Color.cyan);
    }
    string ColorToHex(Color color)
    {
        // Convert each color component to its hexadecimal representation
        int r = (int)(color.r * 255f);
        int g = (int)(color.g * 255f);
        int b = (int)(color.b * 255f);
        int a = (int)(color.a * 255f);

        // Format the hexadecimal string
        string hex = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);

        return hex;
    }
}
