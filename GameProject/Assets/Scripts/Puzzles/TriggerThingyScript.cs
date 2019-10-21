using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerThingyScript : MonoBehaviour
{
    private StatuePuzzleThingy Statue;

    public int ItemsCollected;
    public List<bool> ItemGrabbed;

    private void Start()
    {
        Statue = FindObjectOfType<StatuePuzzleThingy>();
    }
    private void Update()
    {
        if ((ItemsCollected == 2) && (ItemGrabbed[0]) && (ItemGrabbed[1]))
        {

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((Statue.ItemSpawned[0]) && (!ItemGrabbed[0]))
        {
            if (collision.gameObject.tag == "Player")
            {
                ++ItemsCollected;
                ItemGrabbed[0] = true;
            }
        }

        if ((Statue.ItemSpawned[1]) && (!ItemGrabbed[1]))
        {
            if (collision.gameObject.tag == "Player")
            {
                ++ItemsCollected;
                ItemGrabbed[1] = true;
            }
        }

        if ((Statue.ItemSpawned[2]) && (!ItemGrabbed[2]))
        {
            if (collision.gameObject.tag == "Player")
            {
                ++ItemsCollected;
                ItemGrabbed[2] = true;
            }
        }

    }
}
