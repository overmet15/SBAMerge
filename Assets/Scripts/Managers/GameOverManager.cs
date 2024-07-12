using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gmPanel;
    public void GameOver()
    {
        Manager.instance.canPlay = false;
        gmPanel.SetActive(true);
    }
    public void GameOverWarning()
    {
    }
}
