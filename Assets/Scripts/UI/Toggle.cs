using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Toggle : MonoBehaviour
{
    public bool Toggled;
    public UnityEvent<bool> OnValueChanged;
    [SerializeField] private Image image;
    [SerializeField] private Color offlineColor;
    [SerializeField] private Color onlineColor;


    public void OnClick()
    {
        Toggled = !Toggled;
        if (!Toggled) image.color = offlineColor;
        else image.color = onlineColor;
        OnValueChanged.Invoke(Toggled);
    }
    public void Set(bool value)
    { 
        Toggled = value;
        if (!Toggled) image.color = offlineColor;
        else image.color = onlineColor; 
        OnValueChanged.Invoke(Toggled);
    }
}
