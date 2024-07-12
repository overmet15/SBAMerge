using TMPro;
using UnityEngine;

public class Translate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public string key;
    public string output;
    public bool applyText = true;
    void Start()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();
        ChangeTranslation();
    }
    void OnEnable()
    {
        text.font = TranslationManager.font;
        ChangeTranslation();
    }
    public void ChangeTranslation()
    {
        text.font = TranslationManager.font;
        output = TranslationManager.Translate(key);
        if (text != null && applyText) text.text = output;
    }
}
