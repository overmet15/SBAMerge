using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    void Update()
    {
        text.text = "x" + SaveValues.instance.gameData.coinCount.ToString();
    }
}
