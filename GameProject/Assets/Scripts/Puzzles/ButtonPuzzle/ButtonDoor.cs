using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonDoor : MonoBehaviour
{
   
    public int buttons;
    [HideInInspector]
    public int Buttons;
    void Update()
    {
        if(Buttons >= buttons)
        {
            gameObject.SetActive(false);
        }
    }
}