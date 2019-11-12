using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    InputManager IM;
    Quest[] allQuests;
    Quest activeQuest;
    private void Start()
    {
        IM = FindObjectOfType<InputManager>();
        allQuests = GameObject.FindObjectsOfType<Quest>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Hero"))
        {
            activeQuest = getActiveQuests();
            if (IM.Button_A())
            {
                for(int i=0;i<activeQuest.ItemsToBeCollected.Length;i++)
                {
                    if (activeQuest.ItemsToBeCollected[i] == gameObject.GetComponent<SpriteRenderer>().sprite)
                    { 
                        activeQuest.ItemsQuantities[i]--;
                        Destroy(gameObject);

                    }
                }
            }
        }
    }

    private Quest getActiveQuests()
    {
        foreach (Quest quest in allQuests)
            if (quest.CollectRequest && quest.isActiveAndEnabled)
                return quest; 
        return null;
    }
}
