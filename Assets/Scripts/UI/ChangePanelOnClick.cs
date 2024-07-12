using UnityEngine;

public class ChangePanelOnClick : MonoBehaviour
{
    [SerializeField] private GameObject toDisable;
    [SerializeField] private GameObject toActive;
    public void OnClick()
    {
        if (toDisable != null) toDisable.SetActive(false);
        if (toActive != null) toActive.SetActive(true);
    }
}
