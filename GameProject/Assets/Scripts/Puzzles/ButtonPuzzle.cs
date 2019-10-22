﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    public Sprite NewSprite;
    public ButtonPuzzle Button1;
    public ButtonPuzzle Button2;
    public bool Pressed;
    public bool Achieved;
    // Start is called before the first frame update
    void Start()
    {
        Pressed = false;
    }
    public bool ButtonPressed()
    {
        if (Pressed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Pressed = true;
        if (Button1.ButtonPressed() && Button2.ButtonPressed() && ButtonPressed())
        {
            Achieved = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = NewSprite;
        }
        else
        {
            Achieved = false;
        }     
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Achieved)
        {
            Pressed = false;
        }
    }
}
