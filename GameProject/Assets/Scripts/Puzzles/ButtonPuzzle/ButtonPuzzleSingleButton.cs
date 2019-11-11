using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonPuzzleSingleButton : MonoBehaviour
{
    private bool hit;
    public Sprite newSprite;
    public Sprite OldSprite;
    public ButtonDoor Hit;
    int MaxLeaverUses;
    public bool[] ColourArray;
    public bool ColourPuzzle;
    SpriteRenderer MySprite;
    GameObject MyObject;
    void Start()
    {
        hit = false;
        MySprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (MaxLeaverUses < ColourArray.Length)
        {
            if (ColourPuzzle)
            {
                if (ColourArray[Hit.Buttons])
                {
                    GameObject[] Leaver = GameObject.FindGameObjectsWithTag("Leaver");
                    for (int i = 0; i < Leaver.Length; i++)
                    {
                        Leaver[i].GetComponent<ButtonPuzzleSingleButton>().MaxLeaverUses++;
                    }
                    Hit.Buttons++;
                    spriteChange();
                }
                else
                {
                    Hit.Buttons = 0;
                    GameObject[] Sprites = GameObject.FindGameObjectsWithTag("Leaver");
                    for (int i = 0; i < Sprites.Length; i++)
                    {
                        Sprites[i].GetComponent<ButtonPuzzleSingleButton>().RevertSprite();
                        Sprites[i].GetComponent<ButtonPuzzleSingleButton>().MaxLeaverUses = 0;
                    }
                }
            }
            else
            {
                hit = true;
                Hit.Buttons++;
                spriteChange();
            }
        }
    }
    public void RevertSprite()
    {
        MySprite.sprite = OldSprite;
    }
    private void spriteChange()
    {
        MySprite.sprite = newSprite;
        if (!ColourPuzzle)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}