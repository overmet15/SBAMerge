using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private Translate t0;
    private Translate t1;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI hiScoreText;
    void Start()
    {
        t0 = scoreText.GetComponent<Translate>();
        t1 = hiScoreText.GetComponent<Translate>();
    }
    void Update()
    {
        if (SaveValues.instance != null)
        {
            hiScoreText.text = t1.output + ": " + SaveValues.instance.gameData.mainModeScore;
        }
        scoreText.text = t0.output + ": \n" + Manager.Instance.score;
    }
}
