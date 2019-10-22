using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonPuzzleSingleButton : MonoBehaviour
{
    private bool hit;
    public Sprite newSprite;
    // Start is called before the first frame update
    void Start()
    {
        hit = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
