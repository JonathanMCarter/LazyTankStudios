using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileLight : MonoBehaviour
{
    private bool Lit;
    public LightDoor Door;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(Lit)
            {
                Door.LitTiles++;
            }
            else
            {
                Door.LitTiles--;
            }
        }
    }
}