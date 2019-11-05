using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonPuzzleSingleButton : MonoBehaviour
{
    private bool hit;
    public Sprite newSprite;
    public Sprite OldSprite;
    public ButtonDoor Hit;
    public bool[] ColourArray;
    public bool ColourPuzzle;
    // Start is called before the first frame update


        //Need to find a way to change all sprites of all switches back if the wrong one is pressed
    void Start()
    {
        hit = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (ColourPuzzle)
        {
            if (ColourArray[Hit.Buttons])
            {
                Hit.Buttons++;
                hit = true;
                spriteChange();
            }
            else
            {
                Hit.Buttons = 0;
            }
        }
        else
        {
            hit = true;
            Hit.Buttons++;
            spriteChange();
        }
    }
    private void spriteChange()
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
        if (ColourPuzzle)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}