using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightDoor : MonoBehaviour
{
    
    public int TotalTiles;
    public BoxCollider2D Door;
    public Sprite Open;
    public Sprite Closed;
    public SpriteRenderer me;
    [HideInInspector]
    public int LitTiles;
    // Update is called once per frame
    private void Start()
    {
        me = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(LitTiles >= TotalTiles)
        {
            Door.enabled = false;
            me.sprite = Open;
        }
        else
        {
            Door.enabled = true;
            me.sprite = Closed;
        }
    }
}