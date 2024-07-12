using System.Collections;
using TMPro;
using UnityEngine;

public class RowText : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private AnimationCurve curve2;
    [SerializeField] private GameObject p;
    [SerializeField] private TextMeshProUGUI xText, scoreText;
    public void CreateRow(int rowLvl, int scorePlus)
    {
        StartCoroutine(enumerator());
        xText.text = "x" + rowLvl.ToString();
        scoreText.text = "+" + scorePlus.ToString();
    }

    IEnumerator enumerator()
    {
        float elapsedTime = 0;
        while (elapsedTime < 1) 
        {
            elapsedTime += Time.deltaTime;
            p.transform.localPosition = new Vector2(0, curve.Evaluate(elapsedTime));
            p.GetComponent<CanvasGroup>().alpha = curve2.Evaluate(elapsedTime);

            yield return null;
        }
        Destroy(gameObject);
    }
}
