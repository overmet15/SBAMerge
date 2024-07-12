using TMPro;
using UnityEngine;

public class TranslateChangeControls : MonoBehaviour
{

	[SerializeField] private TextMeshProUGUI text;
	[SerializeField] private string changeControlKey;
	[SerializeField] private string KeyboardKey;
	[SerializeField] private string MouseKey;
	public string output;
    private void OnEnable()
    {
        ChangeTranslation();
    }

    public void ChangeTranslation()
	{
		output = string.Concat(str2: (!SaveValues.instance.optionsData.keyboardControls) ? TranslationManager.Translate(MouseKey) : TranslationManager.Translate(KeyboardKey), str0: TranslationManager.Translate(changeControlKey), str1: ": \n");
		text.font = TranslationManager.font;
		text.text = output;
	}
}
