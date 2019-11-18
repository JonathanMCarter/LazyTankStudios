﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
//Created by Gabriel Potamianos
//Date:
//Last edit: 15th Nov 2019
//Reason: Bug fixing throughout the script -SideQuests-
public class Quest : A
{
    static public bool[] boss = new bool[8];
    public int ID;
    public enum Type { Return, NonReturn };
    public Type type;
    public bool SideQuest;
    public GameObject NPCToReturnTo;
    public DialogueFile Dialogue;
    public DialogueFile QuestCompleted;
    public bool KillRequest;
    public bool CollectRequest;
    public bool DeliverRequest;
    public List<GameObject> Kills = new List<GameObject>();
    public Sprite[] ItemsToBeCollected;
    public int[] ItemsQuantities;
    public int DeliverGold;
    public enum Reward { Items, Gold, GoldAndItems }
    public Reward reward;
    public Sprite[] rewardItems;
    GameObject newItem;
    public Vector2[] rewardIdsAndQuantities;
    public int rewardGold;
    public enum Status { NotAvailable, Completed, Available, OnGoing, ReadyToComplete };
    public Status status;
    TalkScript GameManagerTalk;
    Collider2D[] colliders = new Collider2D[10];
    Quest[] quests;
    Inventory inv;
    static public int currQuest = 0;
    GameObject ActiveQuestSign;
    private PlayerMovement HeroRef;
    private DialogueScript ds;
    private void InitialiseQuests(Status StatusToSet)
    {
        status = ID == HeroRef.QuestActiveID ? StatusToSet : Status.NotAvailable;
        gameObject.transform.GetComponent<BoxCollider2D>().enabled = status == StatusToSet ? true : false;
        NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = status == StatusToSet ? true : false;
        enabled = status == StatusToSet;
        ActiveQuestSign.SetActive(status == StatusToSet);

    }
    void Awake()
    {
        ds = GameObject.FindObjectOfType<DialogueScript>();
        HeroRef = GameObject.FindObjectOfType<PlayerMovement>();
        HeroRef.QuestActiveID = currQuest;
        ActiveQuestSign = gameObject.transform.parent.GetChild(1).gameObject;
        newItem = Resources.Load<GameObject>("New Item");
        inv = FindObjectOfType<Inventory>();
        quests = FindObjectsOfType<Quest>();
        GameManagerTalk = GameObject.Find("GameManager").GetComponent<TalkScript>();
        InitialiseQuests(Status.Available);
    }
    private void Start()
    {
        if (ID != 0)
            InitialiseQuests(Status.OnGoing);
    }
    void Update()
    {
        if (NPCToReturnTo && type.Equals(Type.Return))
        {
            if (NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, colliders) > 1
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
                if (type == Type.Return)
                    NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (status != Status.ReadyToComplete)
        { 
            if (collision.gameObject.name == "Hero" && isActiveAndEnabled)
            {
                GetComponent<TalkScript>().dialogueEnglish = Dialogue;
                if (DeliverRequest && status == Status.Available)
                    inv.addCoins(DeliverGold);
                status = Status.OnGoing;
            }
        }
    }
    bool checkKilledAllEnemies()
    {
        bool state = false;
        if (Kills.Count > 0)
            foreach (GameObject enemy in Kills)
                state = enemy.activeInHierarchy ? false : true;
        else
        {
            state = boss[NewAIMove.currBoss] == true;
            if (state)
                NewAIMove.currBoss++;
        }
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
                GameManagerTalk.dialogueEnglish = QuestCompleted;
                GameManagerTalk.talk();
                offerReward(reward);
            }
            else
                Debug.LogError("This " + ID + " quest has failed because you spend all the money");
        }
        else
        {
            GameManagerTalk.dialogueEnglish = QuestCompleted;
            GameManagerTalk.talk();
            offerReward(reward);
        }
        status = Status.Completed;
    }
    public void findNextQuest()
    {
        bool lastQuest = true;
        foreach (Quest q in quests)
        {
            if (q.ID == ID + 1 && !SideQuest)
            {
                q.enabled = q.gameObject.transform.GetComponent<BoxCollider2D>().enabled = true;
                enabled = false;
                if (q.gameObject != this.gameObject)
                    gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
                lastQuest = false;
                currQuest++;
                HeroRef.QuestActiveID = currQuest;
                q.ActiveQuestSign.SetActive(true);
                ActiveQuestSign.SetActive(false);
                q.status = Status.Available;
            }
            else if (SideQuest && currQuest >= q.ID && q.status != Status.Completed)
            {
                enabled = gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
                ActiveQuestSign.SetActive(false);
                q.status = Status.Available;
                lastQuest = false;
            }
        }
        if (lastQuest)
        {
            enabled = gameObject.transform.GetComponent<BoxCollider2D>().enabled = NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            ActiveQuestSign.SetActive(false);
        }
    }
    public void offerReward(Quest.Reward typeOfReward)
    {
        Random.InitState(Random.Range(0, 100));
        switch (typeOfReward)
        {
            case Reward.Items:
                    throwItems();
                    break;
            case Reward.Gold:
                inv.addCoins(rewardGold);
                break;
            case Reward.GoldAndItems:
                    throwItems();
                    inv.addCoins(rewardGold);
                    break;
            default:
                break;
        }
    }
    private void throwItems()
    {
        Vector3 updatedPosition = type == Type.Return ? NPCToReturnTo.transform.position : GameObject.Find("Hero").transform.position;
        for (int i = 0; i < rewardItems.Length; i++)
        {
            GameObject temp = Instantiate(newItem, updatedPosition, Quaternion.identity);
            temp.GetComponent<SpriteRenderer>().sprite = rewardItems[i];
            temp.GetComponent<SpriteRenderer>().sortingOrder = 20;
            temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            if ((int)rewardIdsAndQuantities[i].x >= 0 && (int)rewardIdsAndQuantities[i].x < 12)
                temp.GetComponent<InventoryItem>().ID = (int)rewardIdsAndQuantities[i].x;
            if ((int)rewardIdsAndQuantities[i].y > 0)
                temp.GetComponent<InventoryItem>().quantity = (int)rewardIdsAndQuantities[i].y;
            temp.AddComponent<Rigidbody2D>();
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-150, 150), Random.Range(-150, 150)));
            StartCoroutine(freezeItemGravity(temp.GetComponent<Rigidbody2D>()));
        }
    }
    IEnumerator freezeItemGravity(Rigidbody2D temporaryObject)
    {
        yield return new WaitForSeconds(0.5f);
        temporaryObject.gravityScale = 0;
        temporaryObject.bodyType = RigidbodyType2D.Static;
    }
}