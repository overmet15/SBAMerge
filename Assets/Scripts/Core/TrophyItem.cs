using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrophyItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private List<Image> toColor = new();
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private CanvasGroup bg;
    public void SetUp(Color color, Sprite img, string name, string desc, bool unlocked)
    {
        foreach (Image imgg in toColor) imgg.color = color;
        image.sprite = img;
        nameText.text = name;
        descText.text = TranslationManager.Translate(desc);
        /*nameText.font = */descText.font = TranslationManager.font;
        if (!unlocked) bg.alpha = 0.6f;
        else bg.alpha = 1f;
    }
}
