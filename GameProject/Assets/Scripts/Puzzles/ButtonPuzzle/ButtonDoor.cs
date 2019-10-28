using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonDoor : MonoBehaviour
{
    public int Buttons;
    void Update()
    {
        if(Buttons >= 4)
        {
            gameObject.SetActive(false);
        }
    }
}