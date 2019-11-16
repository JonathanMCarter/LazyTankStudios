using UnityEngine;
public class ButtonPuzzleSingleButton : A
{
     bool hit = false;
    public Sprite newSprite,OldSprite;
    public ButtonDoor Hit;
    int MaxLeaverUses;
    public bool[] ColourArray;
    public bool ColourPuzzle;
    SpriteRenderer MySprite;

    void Start()
    {
        MySprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (ColourPuzzle)
        {
            if (MaxLeaverUses < ColourArray.Length)
            {
                if (ColourArray[Hit._Buttons])
                {
                    GameObject[] Leaver = GameObject.FindGameObjectsWithTag("Leaver");
                    for (int i = 0; i < Leaver.Length; i++)
                    {
                        Leaver[i].GetComponent<ButtonPuzzleSingleButton>().MaxLeaverUses++;
                    }
                    Hit._Buttons++;
                    spriteChange();
                }
                else
                {
                    Hit._Buttons = 0;
                    GameObject[] Sprites = GameObject.FindGameObjectsWithTag("Leaver");
                    for (int i = 0; i < Sprites.Length; i++)
                    {
                        Sprites[i].GetComponent<ButtonPuzzleSingleButton>().RevertSprite();
                        Sprites[i].GetComponent<ButtonPuzzleSingleButton>().MaxLeaverUses = 0;
                    }
                }
            }
        }
        else
        {
            hit = true;
            Hit._Buttons++;
            spriteChange();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public void RevertSprite()
    {
        MySprite.sprite = OldSprite;
    }
    private void spriteChange()
    {
        MySprite.sprite = newSprite;
        if (!ColourPuzzle) GetComponent<BoxCollider2D>().enabled = false;

    }
}