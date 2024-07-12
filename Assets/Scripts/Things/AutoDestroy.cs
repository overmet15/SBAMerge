using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float timer;
    void Start()
    {
        StartCoroutine(AutoDest());
    }

    private IEnumerator AutoDest()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
