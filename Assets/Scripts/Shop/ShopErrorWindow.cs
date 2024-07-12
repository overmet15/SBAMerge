using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopErrorWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private GameObject _panel;

    public void SetWindowThingy(string main, string secondary)
    {
        nameText.text = main;
        descText.text = secondary;
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
            _panel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, scaleProgress);
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
            _panel.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, scaleProgress);
            yield return null;
        }
        Destroy(gameObject);
    }
}
