using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromocodeRewardWindow : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI rewardName;
    [SerializeField] private TextMeshProUGUI rewardDescription;
    [SerializeField] private Image img;

    public void ShowReward(string rName, string rDescription, Sprite sprite)
    {
        rewardName.text = rName;
        rewardDescription.text = rDescription;
        img.sprite = sprite;
        StartCoroutine(OnCreate());
    }
    public void ShowReward(string rName, string rDescription, int rReward, Sprite sprite)
    {
        rewardName.text = rName + " x" + rReward.ToString();
        rewardDescription.text = rDescription;
        img.sprite = sprite;
        StartCoroutine(OnCreate());
    }

    public void DoneThing()
    {
        StartCoroutine(OnEnd());
    }

    IEnumerator OnCreate()
    {
        float elapsed = 0;
        float dur = 0.15f;
        while (elapsed < dur)
        {
            elapsed += Time.deltaTime;
            float scaleProgress = elapsed / dur;
            panel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, scaleProgress);
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
    IEnumerator OnEnd()
    {
        float elapsed = 0;
        float dur = 0.15f;
        while (elapsed < dur)
        {
            elapsed += Time.deltaTime;
            float scaleProgress = elapsed / dur;
            panel.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, scaleProgress);
            yield return null;
        }
        Destroy(gameObject);
    }
}
