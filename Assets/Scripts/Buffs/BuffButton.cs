using TMPro;
using UnityEngine;

public class BuffButton : MonoBehaviour
{
    [SerializeField]
    private BuffManager.buffType type;

    [SerializeField]
    private TextMeshProUGUI numText;

    private CanvasGroup group;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        int n = BuffManager.instance.ReturnHowMuchValue(type);
        if (!BuffManager.instance.bombActive && type != BuffManager.buffType.bomb)
        {
            AlphaCount(n);
        }
        else if (type == BuffManager.buffType.bomb)
        {
            AlphaCount(n);
        }
        else
        {
            group.alpha = 0.4f;
        }
    }

    private void AlphaCount(int n)
    {
        if (n != 0)
        {
            group.alpha = 1f;
            numText.text = "x" + n;
            numText.gameObject.SetActive(value: true);
        }
        else
        {
            group.alpha = 0.4f;
            numText.gameObject.SetActive(value: false);
        }
    }

    public void DoStuff()
    {
        BuffManager.instance.ActivateBuff(type);
    }
}
