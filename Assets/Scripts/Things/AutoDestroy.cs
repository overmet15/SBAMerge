using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float timer;
    void Start()
    {
        Destroy(gameObject, timer);
    }
}
