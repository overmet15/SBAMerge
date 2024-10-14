using GameJolt.API;
using GameJolt.API.Objects;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchivementNotifier : MonoBehaviour
{
    private Queue<AchivementNotification> notificationQueue = new();
    private bool busy;
    public bool forceBusy;

    [SerializeField] private Image image;
    [SerializeField] private Image toColor;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    public Color undefined, bronze, silver, gold, platinum;
    private void Start()
    {
        forceBusy = true;
        StartCoroutine(Countdown());
    }
    void Update()
    {
        if (notificationQueue.Count > 0 && !busy && !forceBusy)
        {
            busy = true;
            AchivementNotification notification = notificationQueue.Dequeue();
            Notify(notification.ID);
        }
    }
    public void QueueNotify(int id)
    {
        notificationQueue.Enqueue(new AchivementNotification() { ID = id });
    }
    void Notify(int id)
    {
        Trophies.Get(id, trophy => {
            if (trophy != null)
            {
                SoundManager.instance.NotifySound();
                GetComponent<Animator>().SetTrigger("Go");
                ColorImg(trophy.Difficulty);
                nameText.text = trophy.Title;
                descText.text = TranslationManager.Translate(trophy.Description);
                descText.font = TranslationManager.font;
                if (trophy.Image != null) { image.sprite = trophy.Image; }
                else { trophy.DownloadImage(suc => { if (suc) image.sprite = trophy.Image; }); }
            }
        });
    }

    void ColorImg(TrophyDifficulty dif)
    {
        switch (dif)
        {
            case TrophyDifficulty.Undefined: toColor.color = undefined; break;
            case TrophyDifficulty.Bronze: toColor.color = bronze; break;
            case TrophyDifficulty.Silver: toColor.color = silver; break;
            case TrophyDifficulty.Gold: toColor.color = gold; break;
            case TrophyDifficulty.Platinum: toColor.color = platinum; break;
        }
    }
    // USED BY ANIMATION!!!
    public void SetNonBusy()
    {
        busy = false;
    }
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(7);
        forceBusy = false;
    }
}

public class AchivementNotification
{
    public int ID;
}
