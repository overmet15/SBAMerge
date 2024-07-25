using GameJolt.API;
using UnityEngine;

public class GamejoltManager : MonoBehaviour
{
    public static GamejoltManager instance;
    private bool points1, points2, points3, points4, row, lastBall;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ResetAchivementCounters();
    }
    private void Update()
    {
        if (Manager.instance.score >= 100 && !points1) { Core.instance.UnlockAchivement(232898); points1 = true; }
        if (Manager.instance.score >= 1000 && !points2) { Core.instance.UnlockAchivement(232899); points2 = true; }
        if (Manager.instance.score >= 10000 && !points3) { Core.instance.UnlockAchivement(232900); points3 = true; }
        if (Manager.instance.score >= 100000 && !points4) { Core.instance.UnlockAchivement(232901); points4 = true; }
    }
    public void ResetAchivementCounters()
    {

        points1 = points2 = points3 = points4 = row = lastBall = false; // I Dont Want To Make Infinity Loop.
    }
    public void UnlockRowAchivement()
    {
        if (!row) { Core.instance.UnlockAchivement(233633); row = true; }
    }
    public void UnlockLastBallAchivement()
    {
        if (!lastBall) { Core.instance.UnlockAchivement(234744); lastBall = true; }
    }
    public void SignIn()
    {
        Core.instance.SignIn();
    }

    public void SignOut()
    {
        var isSignedIn = GameJoltAPI.Instance.CurrentUser != null;
        if (isSignedIn)
        {
            GameJoltAPI.Instance.CurrentUser.SignOut();
        }
    }

    public void SaveData()
    {
        Core.instance.OpenDataManager();
    }
}
