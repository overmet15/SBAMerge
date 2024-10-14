using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gmPanel;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private CanvasGroup normalUI, fullscreenUI;
    public void GameOver()
    {
        StartCoroutine(HideUIAnim(1f, normalUI));
        StartCoroutine(HideUIAnim(1f, fullscreenUI));

        string GMText = string.Empty;
        if (Manager.Instance.score > SaveValues.instance.gameData.mainModeScore)
            GMText += $"<color=yellow>{TranslationManager.Translate("NewRecord")}</color>\n\n";

        GMText += $"{TranslationManager.Translate("Score")}: {Manager.Instance.score}\n";
        GMText += $"{TranslationManager.Translate("PrevScore")}: {SaveValues.instance.gameData.mainModeScore}\n\n";

        GMText += $"{TranslationManager.Translate("Merged")}: {Manager.Instance.ballsMerged}\n";
        GMText += $"{TranslationManager.Translate("PrevMerged")}: {SaveValues.instance.gameData.MostMergedBalls}\n";
        text.text = GMText;
        Manager.Instance.canPlay = false;
        gmPanel.SetActive(true);
    }
    private IEnumerator HideUIAnim(float duration, CanvasGroup target) // Stole code from Mergeable.cs
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            target.alpha = Mathf.Lerp(1, 0, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.alpha = 0;
    }
}
