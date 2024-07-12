using TMPro;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textC;

    void Start()
    {
        string text = $"Running On {Application.platform} Version: {Application.version}";
        textC.text = text;
    }
}
