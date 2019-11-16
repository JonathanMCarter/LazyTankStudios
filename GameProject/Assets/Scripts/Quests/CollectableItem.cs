using UnityEngine;

public class CollectableItem : A
{
    InputManager IM;
    Quest[] allQuests;
    Quest activeQuest;
     void Start()
    {
        IM = FindObjectOfType<InputManager>();
        allQuests = FindObjectsOfType<Quest>();
    }

   void OnTriggerStay2D(Collider2D collision)
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
