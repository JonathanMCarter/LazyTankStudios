using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightDoor : MonoBehaviour
{
    public int LitTiles;
    public int TotalTiles;
    public BoxCollider2D Door;
   // Update is called once per frame
    void Update()
    {
        if(LitTiles >= TotalTiles)
        {
            Door.enabled = false;
        }
        else
        {
            Door.enabled = true;
        }
    }
}