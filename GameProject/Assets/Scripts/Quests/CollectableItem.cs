using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    InputManager IM;
    Quest activeQuest;
    private void Start()
    {
        IM = FindObjectOfType<InputManager>();
        activeQuest = GameObject.FindObjectOfType<Quest>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Hero"))
        {

            if (IM.Button_A())
            {
                for(int i=0;i<activeQuest.ItemsToBeCollected.Length;i++)
                {
                    if (activeQuest.ItemsToBeCollected[i] == gameObject.GetComponent<SpriteRenderer>().sprite)
                    { 
                        activeQuest.ItemsQuantities[i]++;
                        Destroy(gameObject);

                    }
                }
            }
        }
    }
}
