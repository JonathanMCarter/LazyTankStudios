using UnityEngine;

public class ButtonPuzzle : A
{
    public Sprite NewSprite;
    public ButtonPuzzle Button1,Button2;
    bool Pressed = false, Achieved;
    public ButtonDoor Hit;


    //void Start()
    //{
    //    Pressed = false;
    //}

    public bool ButtonPressed()
    {
        return (Pressed);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        Pressed = true;
        if (Button1.ButtonPressed() && Button2.ButtonPressed() && ButtonPressed())
        {
            Achieved = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = NewSprite;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Hit._Buttons++;
        }
        else
        {
            Achieved = false;
        }     
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Achieved)Pressed = false;
    }
}