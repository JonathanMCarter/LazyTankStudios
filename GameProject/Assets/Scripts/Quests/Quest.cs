using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    public int ID;
    public enum Type { Return, NonReturn };
    public Type type;

    public GameObject NPCToReturnTo;
    public DialogueFile Dialogue;
    public DialogueFile QuestCompleted;

    public bool KillRequest, CollectRequest, DeliverRequest;

    [Space(20)]
    [SerializeField]
    public List<GameObject> Kills = new List<GameObject>();

    [Space(10)]
    public Sprite[] ItemsToBeCollected;
    public int[] ItemsQuantities;
    //public int DeliverGold;

    //public enum Reward { Items, Gold }
    //public Reward reward;
    //public GameObject[] Items;
    //public Sprite[] ItemsSprites;
    //public int GoldReceived;
    public enum Status { NotAvailable, Available, OnGoing, Completed };

    
    public Status status;
    private DialogueScript ds;


    Collider2D[] colliders = new Collider2D[10];
    ContactFilter2D contactFilter = new ContactFilter2D();




    private void Awake()
    {
        ds = GameObject.Find("DialogueHandler").GetComponent<DialogueScript>();
        status = Status.NotAvailable;
        if (ID == 0)
            status = Status.Available;
    }
    private void Start()
    {
;

        //for (int i = 0; i < Items.Length; i++)
        //    Items[i].GetComponent<SpriteRenderer>().sprite = ItemsSprites[i];
    }

    private void Update()
    {

        if (NPCToReturnTo)
        {
            if (NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, colliders) > 1
                && status.Equals(Status.Completed))
            {
                displayQuestCompletedDialogue();
                findNextQuest();
            }
        }
        else if (status.Equals(Status.Completed))
        {
            displayQuestCompletedDialogue();
            findNextQuest();
        }


        if (status == Status.OnGoing)
            if (checkKilledAllEnemies())
                status = Status.Completed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero" && this.isActiveAndEnabled)
        {
            ds.ChangeFile(Dialogue);
            status = Status.OnGoing;
        }
    }



    private bool checkKilledAllEnemies()
    {
        bool state = false;

        if (Kills.Count > 0)
            foreach (GameObject enemy in Kills)
                if (!enemy.activeInHierarchy)
                    state = true;

        return state;
    }


    void displayQuestCompletedDialogue()
    {
        TalkScript talk = GameObject.Find("GameManager").GetComponent<TalkScript>();
        ds.ChangeFile(QuestCompleted);
        talk.dialogue = QuestCompleted;
        talk.talk();
    }

    public void findNextQuest()
    {
        Quest[] quests = GameObject.FindObjectsOfType<Quest>();

        bool lastQuest = true;

        foreach (Quest component in quests)
        {
            if (component.ID == this.ID + 1)
            {
                component.enabled=true;
                component.status = Status.Available;
                this.enabled = false;
                lastQuest = false;

            }
        }

        if (lastQuest)
        {
            this.enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }


    }
}




