using System.Linq;
using UnityEngine;

public class LeaderboardWindow : MonoBehaviour
{
    [SerializeField] private GameObject scoreItem;
    [SerializeField] private RectTransform rect;
    public void ToggleWindow()
    {
        GetComponent<Animator>().SetTrigger("Go");
    }
    public void SetUp(int tableID)
    {
        GameJolt.API.Scores.Get(scores => {
        if (rect.childCount != 0) RemoveTables();
        int count = 0;
            if (scores != null)
            {
                foreach (var score in scores)
                {
                    var tab = Instantiate(scoreItem, rect).GetComponent<ScoreItem>();
                    tab.text.text = score.PlayerName + ": " + score.Value;
                    tab.count = count;
                    count++;
                }
            }
            else Instantiate(scoreItem, rect).GetComponent<ScoreItem>().text.text = "ERR";
        }, tableID, 50);
    }

    void RemoveTables()
    {
        for (int i = rect.childCount - 1; i >= 0; i--)
        {
            Destroy(rect.GetChild(i).gameObject);
        }
    }
}
