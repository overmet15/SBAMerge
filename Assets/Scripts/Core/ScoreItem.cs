using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ScoreItem : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int count;
    [SerializeField] private Color[] colors;
    [SerializeField] private List<Image> toColor = new();

    private void Start()
    {
        Color colorToUse = (count < colors.Length) ? colors[count] : colors[colors.Length - 1];
        foreach (Image img in toColor) img.color = colorToUse;
    }
}
