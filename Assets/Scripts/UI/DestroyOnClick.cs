using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DestroyOnClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject objectToDestroy;

    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(objectToDestroy);
    }
}
