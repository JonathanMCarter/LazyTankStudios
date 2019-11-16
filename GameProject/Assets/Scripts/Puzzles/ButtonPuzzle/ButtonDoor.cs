
using UnityEngine;
public class ButtonDoor : A
{
    public int buttons;
    [HideInInspector]
    public int _Buttons = 0;


    void Update()
    {
        if(_Buttons >= buttons)
        {
            gameObject.SetActive(false);
        }
    }
}