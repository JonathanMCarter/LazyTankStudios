using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonDoor : MonoBehaviour
{
    public int buttons;
    [HideInInspector]
    public int _Buttons;
    void Start()
    {
        _Buttons = 0;
    }
    void Update()
    {
        if(_Buttons >= buttons)
        {
            gameObject.SetActive(false);
        }
    }
}