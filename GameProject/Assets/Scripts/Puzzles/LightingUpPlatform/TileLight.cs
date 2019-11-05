using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileLight : MonoBehaviour
{
    public bool Lit;
    public LightDoor Door;
    public Sprite On;
    public Sprite Off;
    private void Start()
    {
        Door = GameObject.FindGameObjectWithTag("Door").GetComponent<LightDoor>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!Lit)
            {
                Door.LitTiles++;
                Lit = true;
                GetComponent<SpriteRenderer>().sprite = On;
            }
            else
            {
                Door.LitTiles--;
                Lit = false;
                GetComponent<SpriteRenderer>().sprite = Off;
            }
        }
    }
}