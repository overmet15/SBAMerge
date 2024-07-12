using UnityEngine;
using GameJolt.API.Objects;
using GameJolt.API;
using System.Linq;
using TMPro;

public class TrophyPanel : MonoBehaviour
{
    [SerializeField] private GameObject trophyItem;
    [SerializeField] private RectTransform rect;
    [SerializeField] public Color undefined, bronze, silver, gold, platinum;
    [SerializeField] private TextMeshProUGUI bronzeText, silverText, goldText, platinumText;
    private int bronzeCount, silverCount, goldCount, platinumCount;
    private int bronzeFin, silverFin, goldFin, platinumFin;

    private bool currentrlyLoading = false;
    public void ToggleWindow()
    {
        GetComponent<Animator>().SetTrigger("Go");
    }

    public void GetItems()
    {
        if (currentrlyLoading || !Core.instance.signedIn) return;
        currentrlyLoading = true;
        Core.instance.ToggleLoadingIndicator(currentrlyLoading);
        Trophies.Get(trophies => {
            if (rect.childCount != 0) RemoveItems(); 
            ResetCount();
            if (trophies != null)
            {
                foreach (Trophy trophy in trophies.Reverse())
                {
                    if (trophy.Image == null) trophy.DownloadImage(b =>
                    {
                        Instantiate(trophyItem, rect).GetComponent<TrophyItem>().SetUp(ColorImg(trophy.Difficulty), trophy.Image, trophy.Title, trophy.Description, trophy.Unlocked);
                    });
                    else Instantiate(trophyItem, rect).GetComponent<TrophyItem>().SetUp(ColorImg(trophy.Difficulty), trophy.Image, trophy.Title, trophy.Description, trophy.Unlocked);
                    RegisterTrop(trophy.Difficulty, trophy.Unlocked);
                }
                currentrlyLoading = false; 
                ToggleWindow();
                Core.instance.ToggleLoadingIndicator(currentrlyLoading);
            }
            else
            {
                currentrlyLoading = false;
                Core.instance.ToggleLoadingIndicator(currentrlyLoading);
                Debug.Log("ERR");
            }
        });
    }
    void RemoveItems()
    {
        for (int i = rect.childCount - 1; i >= 0; i--)
        {
            Destroy(rect.GetChild(i).gameObject);
        }
    }
    Color ColorImg(TrophyDifficulty dif)
    {
        if (dif == TrophyDifficulty.Undefined) return undefined;
        else if (dif == TrophyDifficulty.Bronze) return bronze;
        else if (dif == TrophyDifficulty.Silver) return silver;
        else if (dif == TrophyDifficulty.Gold) return gold;
        else if (dif == TrophyDifficulty.Platinum) return platinum;
        else return Color.red;
    }
    void RegisterTrop(TrophyDifficulty dif, bool unlocked)
    {
        if (dif == TrophyDifficulty.Bronze)
        {
            bronzeCount++;
            if (unlocked) bronzeFin++;
        }
        else if (dif == TrophyDifficulty.Silver)
        {
            silverCount++;
            if (unlocked) silverFin++;
        }
        else if (dif == TrophyDifficulty.Gold)
        {
            goldCount++;
            if (unlocked) goldFin++;
        }
        else if (dif == TrophyDifficulty.Platinum)
        {
            platinumCount++;
            if (unlocked) platinumFin++;
        }
        bronzeText.text = $"{bronzeFin}/{bronzeCount}";
        silverText.text = $"{silverFin}/{silverCount}";
        goldText.text = $"{goldFin}/{goldCount}";
        platinumText.text = $"{platinumFin}/{platinumCount}";
    }
    void ResetCount()
    {
        bronzeCount = silverCount = goldCount = platinumCount = bronzeFin = silverFin = goldFin = platinumFin = 0;
    }
}
