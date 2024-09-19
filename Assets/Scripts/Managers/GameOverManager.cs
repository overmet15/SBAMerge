using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gmPanel;
    [SerializeField] private TextMeshProUGUI text;
    public void GameOver()
    {
        Manager.Instance.canPlay = false;
        gmPanel.SetActive(true);
    }
}
