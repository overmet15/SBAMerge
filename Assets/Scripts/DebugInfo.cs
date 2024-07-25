using System.Collections;
using TMPro;
using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textC;
    void Update()
    {
        textC.text = $"Running On {Application.platform}, Version: {Application.version}, FPS: {Mathf.RoundToInt(1 / Time.unscaledDeltaTime)}";
    }
}
