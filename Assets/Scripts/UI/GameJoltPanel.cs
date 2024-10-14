using GameJolt.API;
using TMPro;
using UnityEngine;

public class GameJoltPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private GameObject panelUserInfo;
    [SerializeField] private GameObject panelLogInButton;
    [SerializeField] private GameObject logOutButton;
    [SerializeField] private GameObject dataMangementButton;
    [SerializeField] private GameObject trophyButton, trophyButton2;
    private Translate t;
    private string ColorString;
    void Start()
    {
        t = usernameText.GetComponent<Translate>();
    }

    // Update is called once per frame
    void Update()
    {
        var isSignedIn = Core.instance.signedIn;
        panelUserInfo.SetActive(isSignedIn);
        panelLogInButton.SetActive(!isSignedIn);
        logOutButton.SetActive(isSignedIn);
        dataMangementButton.SetActive(isSignedIn);
        trophyButton.SetActive(isSignedIn);
        trophyButton2.SetActive(isSignedIn);
        if (isSignedIn)
        {
            ChangeNameColor();
            usernameText.text = t.output + $" <color={ColorString}>" + GameJoltAPI.Instance.CurrentUser.Name + "</color>";
        }
    }

    public void ChangeNameColor()
    {
        if (GameJoltAPI.Instance.CurrentUser != null)
        {
            if (GameJoltAPI.Instance.CurrentUser.Name == "overmet15") ColorString = ColorToHex(Color.green);
            else if (GameJoltAPI.Instance.CurrentUser.Name == "SashaDandy") ColorString = ColorToHex(Color.red);
            else if (GameJoltAPI.Instance.CurrentUser.Name == "BearsBoi") ColorString = ColorToHex(Color.magenta);
            else ColorString = ColorToHex(Color.cyan);
        }
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
