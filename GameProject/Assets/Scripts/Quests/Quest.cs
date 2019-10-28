using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    public enum Type { Return, NonReturn };
    public Type type;

    public GameObject NPCToReturnTo;
    public DialogueFile Dialogue;
    public DialogueFile QuestCompleted;

    public bool KillRequest, CollectRequest, DeliverRequest, ToPos;

    [SerializeField]
    public List<GameObject> Kills = new List<GameObject>();
    public GameObject[] Collectible;
    public int[] CollectibleAmmount;
    public int DeliverGold;

    public enum Reward { Items, Gold }
    public Reward reward;
    public GameObject[] Items;
    public Sprite[] ItemsSprites;
    public int GoldReceived;
    public enum Status { Available, OnGoing, Completed };

    [HideInInspector]
    public Status status;
    private DialogueScript ds;


    Collider2D[] colliders = new Collider2D[10];
    ContactFilter2D contactFilter = new ContactFilter2D();

    private void Start()
    {

        ds = GameObject.Find("DialogueHandler").GetComponent<DialogueScript>();
        for (int i = 0; i < Items.Length; i++)
            Items[i].GetComponent<SpriteRenderer>().sprite = ItemsSprites[i];
    }

    private void Update() 
    {
        if (NPCToReturnTo)
        {
            if (NPCToReturnTo.GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, colliders) > 0 && status.Equals(Status.Completed))
                displayQuestCompletedDialogue();
        }
        else if(status.Equals(Status.Completed))
            displayQuestCompletedDialogue();


        //else if (status == Status.Completed)
        //{
        //    displayQuestCompletedDialogue();
        //    gameObject.SetActive(false);
        //}

        if (status == Status.OnGoing)
            if (checkKilledAllEnemies())
                status = Status.Completed;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            ds.ChangeFile(Dialogue);
            status = Status.OnGoing;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ds.ChangeFile(QuestCompleted);
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
        talk.dialogue = QuestCompleted;
        talk.talk();
    }

}




