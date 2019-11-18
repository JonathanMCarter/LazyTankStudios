﻿
using UnityEngine;
public class TileLight : A
{
    public bool Lit;
    public LightDoor Door;
    public Sprite On, Off;

    void Start()
    {
        Door = GameObject.FindGameObjectWithTag("Door").GetComponent<LightDoor>();
    }

    void OnTriggerEnter2D(Collider2D collision)
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