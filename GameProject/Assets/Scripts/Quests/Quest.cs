
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]



public class Quest : A
{

    public int rewardGold,DeliverGold,ID;

    public enum Type { Return, NonReturn };
    public Type type;


    public GameObject NPCToReturnTo;

    public DialogueFile Dialogue,QuestCompleted;


    public bool SideQuest,KillRequest,CollectRequest,DeliverRequest;


    public List<GameObject> Kills = new List<GameObject>();

    public Sprite[] ItemsToBeCollected;

    public int[] ItemsQuantities;


    public enum Reward { Items, Gold, GoldAndItems }

    public Reward reward;

    public Sprite[] rewardItems;

    GameObject newItem,ActiveQuestSign;

    public Vector2[] rewardIdsAndQuantities;

    public enum Status { NotAvailable, Completed, Available, OnGoing, ReadyToComplete };

    public Status status;

    TalkScript GMT;

    Collider2D[] colliders = new Collider2D[10];
    ContactFilter2D contactFilter = new ContactFilter2D();

    Quest[] quests;

    Inventory inv;

    static int currQuest = 0;


    void Awake()
    {
        ActiveQuestSign = gameObject.transform.parent.GetChild(1).gameObject;

        newItem = Resources.Load<GameObject>("New Item");

        inv = FindObjectOfType<Inventory>();

        quests = FindObjectsOfType<Quest>();

        GMT = GameObject.Find("GameManager").GetComponent<TalkScript>();

        status = ID == 0 ? Status.Available : Status.NotAvailable;

        gameObject.transform.GetComponent<BoxCollider2D>().enabled = status == Status.Available ? true : false;
        NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = status == Status.Available ? true :false;
        enabled = status == Status.Available;
        ActiveQuestSign.SetActive(status == Status.Available);
    }


    void Update()
    {
        if (NPCToReturnTo && type.Equals(Type.Return))
        {
            if (NPCToReturnTo.GetComponentInChildren<BoxCollider2D>().OverlapCollider(contactFilter, colliders) > 1
                && status.Equals(Status.ReadyToComplete))
            {
                displayQuestCompletedDialogue();
                findNextQuest();
            }
        }
        else if (status.Equals(Status.ReadyToComplete))
        {
            displayQuestCompletedDialogue();
            findNextQuest();
        }

        if (status == Status.OnGoing)

            if (checkKilledAllEnemies() && KillRequest
                ||
                checkItemsCollected() && CollectRequest
                ||
                DeliverRequest && inv.getCoins() >= DeliverGold)
            {

                status = Status.ReadyToComplete;
                
                if(type==Type.Return)
                    NPCToReturnTo.GetComponentInChildren<BoxCollider2D>().enabled = true;
            }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero" && isActiveAndEnabled)
        {
            GetComponent<TalkScript>().dialogueEnglish = Dialogue;

            if (DeliverRequest && status == Status.Available)
                inv.addCoins(DeliverGold);

            status = Status.OnGoing;


        }
    }


   bool checkKilledAllEnemies()
    {

        bool state = false;

        if (Kills.Count > 0)
            foreach (GameObject enemy in Kills)
                state = (!enemy.activeInHierarchy);

        return state;
    }

    bool checkItemsCollected()
    {
        bool state = false;

        foreach (int elementQuantity in ItemsQuantities)
            state = elementQuantity == 0 ? true : false;

        return state;
    }

    void displayQuestCompletedDialogue()
    {
        if (DeliverRequest)
        {
            if (inv.getCoins() >= DeliverGold)
            {
                inv.addCoins(-DeliverGold);
                GMT.dialogueEnglish = QuestCompleted;
                GMT.talk();
                offerReward(reward);
            }

        }

        else
        {

            GMT.dialogueEnglish = QuestCompleted;
            GMT.talk();
            offerReward(reward);

        }
        status = Status.Completed;

    }


    public void findNextQuest()
    {

        bool lastQuest=true;
        foreach (Quest q in quests)
        {
            if (q.ID == ID + 1 && !SideQuest)
            {
                q.enabled = q.gameObject.transform.GetComponent<BoxCollider2D>().enabled = true;
                enabled = GetComponent<BoxCollider2D>().enabled = false;
                lastQuest = false;
                currQuest++;
                q.ActiveQuestSign.SetActive(true);
                ActiveQuestSign.SetActive(false);
                q.status = Status.Available;
            }
            else if(SideQuest && currQuest>=q.ID && q.status!=Status.Completed)
            {
                enabled = GetComponent<BoxCollider2D>().enabled = false;
                ActiveQuestSign.SetActive(false);
                q.status = Status.Available;
                lastQuest = false;
            }
        }

        if (lastQuest)
        {
            enabled =GetComponent<BoxCollider2D>().enabled = NPCToReturnTo.GetComponentInChildren<BoxCollider2D>().enabled = false;
            ActiveQuestSign.SetActive(false);
        }
    }

    public void offerReward(Reward typeOfReward)
    {
       // Random.InitState(Random.Range(0, 100));

       // Vector3 updatedPosition = type == Type.Return ? NPCToReturnTo.transform.position : GameObject.Find("Hero").transform.position;

        switch (typeOfReward)
        {
            case Reward.Items:
                {
                    for (int i = 0; i < rewardItems.Length; i++)
                    {

                        //GameObject temp = Instantiate(newItem, updatedPosition, Quaternion.identity);

                        //temp.GetComponent<SpriteRenderer>().sprite = rewardItems[i];
                        //temp.GetComponent<SpriteRenderer>().sortingOrder = 20;

                        //temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                        //if ((int)rewardIdsAndQuantities[i].x >= 0 && (int)rewardIdsAndQuantities[i].x < 12)
                        //    temp.GetComponent<InventoryItem>().ID = (int)rewardIdsAndQuantities[i].x;

                        //if ((int)rewardIdsAndQuantities[i].y > 0)
                        //    temp.GetComponent<InventoryItem>().quantity = (int)rewardIdsAndQuantities[i].y;

                        //temp.AddComponent<Rigidbody2D>();

                        //temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-150, 150), Random.Range(-150, 150)));

                        //StartCoroutine(freezeItemGravity(temp.GetComponent<Rigidbody2D>()));
                        inv.addItem((int)rewardIdsAndQuantities[i].x, 1, false);
                    }
                    break;


                }

            case Reward.Gold:
                inv.addCoins(rewardGold);
                break;
            case Reward.GoldAndItems:
                {
                    for (int i = 0; i < rewardItems.Length; i++)
                    {
                        inv.addItem((int)rewardIdsAndQuantities[i].x, 1, false);


                        //GameObject temp = Instantiate(newItem, updatedPosition, Quaternion.identity);

                        //temp.GetComponent<SpriteRenderer>().sprite = rewardItems[i];
                        //temp.GetComponent<SpriteRenderer>().sortingOrder = 20;
                        //temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        //if ((int)rewardIdsAndQuantities[i].x >= 0 && (int)rewardIdsAndQuantities[i].x < 12)
                        //    temp.GetComponent<InventoryItem>().ID = (int)rewardIdsAndQuantities[i].x;
                        //if ((int)rewardIdsAndQuantities[i].y > 0)
                        //    temp.GetComponent<InventoryItem>().quantity = (int)rewardIdsAndQuantities[i].y;

                        //temp.AddComponent<Rigidbody2D>();

                        //temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-150, 150), Random.Range(-150, 150)));
                        //StartCoroutine(freezeItemGravity(temp.GetComponent<Rigidbody2D>()));


                    }
                    inv.addCoins(rewardGold);
                    break;
                }
            default:
                break;
        }

    }

    //IEnumerator freezeItemGravity(Rigidbody2D temporaryObject)
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    temporaryObject.gravityScale = 0;
    //    temporaryObject.bodyType = RigidbodyType2D.Static;
    //}








    public bool CheckID(int Input)
        {
        return (Input == ID);

        }


    public void UpdateStatus(int QuestID, Status NewStatus)
    {
        Quest[] Q = FindObjectsOfType<Quest>();
        foreach (Quest q in Q) if (q.ID == QuestID) q.status = NewStatus;
        //    if (FindObjectsOfType<Quest>().Length > 1)
        //    {
        //        for (int i = 0; i < FindObjectsOfType<Quest>().Length; i++)
        //        {
        //            if (FindObjectsOfType<Quest>()[i].ID == QuestID)
        //            {
        //                FindObjectsOfType<Quest>()[i].status = NewStatus;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (FindObjectOfType<Quest>().ID == QuestID)
        //        {
        //            FindObjectOfType<Quest>().status = NewStatus;
        //        }
        //    }
        }
    }




