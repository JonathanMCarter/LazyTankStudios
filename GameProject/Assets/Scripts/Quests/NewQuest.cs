using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class NewQuest : MonoBehaviour
{

    //static public int BossNumber;
    static public bool[] boss = new bool[8];
    static public int currQuest = 0;
    static float currTime = 0;
    static GameObject currQuestGO;
    static Status currQuestStatus;
    static List<NewQuest> quests = new List<NewQuest>(); 

    public int ID;
    public enum Type
    {
        Return,
        NonReturn
    };
    public Type type;
    public bool SideQuest;
    public GameObject NPCToReturnTo;
    public DialogueFile Dialogue;
    public DialogueFile QuestCompleted;
    //public bool KillRequest;
    //public bool CollectRequest;
    public bool DeliverRequest;
    //public List<GameObject> Kills = new List<GameObject>();
    //public Sprite[] ItemsToBeCollected;
    //public int[] ItemsQuantities;
    public int DeliverGold;
    //public enum Reward
    //{
    //    Items,
    //    Gold,
    //    GoldAndItems
    //}
    //public Reward reward;
    //public Sprite[] rewardItems;
    //GameObject newItem;
    //public Vector2[] rewardIdsAndQuantities;
    //public int rewardGold;
    public enum Status
    {
        Unavailable,
        Completed,
        Available,
        OnGoing,
        ReadyToComplete
    };
    public Status status;
    Collider2D[] colliders = new Collider2D[10];
    Inventory inv;
    GameObject ActiveQuestSign;
    DialogueScript ds;
    bool startQuest = false;
    ContactFilter2D contactFilter;
    List<NewQuest> NPC_Quests;
    static public bool newSceneLoaded = false;

    public static void orderList()
    {
        quests.AddRange(GameObject.FindObjectsOfType<NewQuest>());
        quests=quests.OrderBy(x => x.ID).ToList();
    }
    void Trigger1QuestOnly(GameObject target)
    {
        List<NewQuest> list=GameObject.FindObjectsOfType<NewQuest>().Where(x=> x.status==Status.Available && x.gameObject == target.gameObject).ToList();
        if (list.Count >= 2)
        {
            list.ForEach(x => x.enabled = false);
            NewQuest temporary = list.FirstOrDefault(x => !x.SideQuest);
            if (temporary)
                temporary.enabled = true;
        }
        else list.ForEach(x => x.enabled = true);
    }
    private void Awake()
    {
        print("WOKRS");
        ActiveQuestSign = transform.parent.GetChild(1).gameObject;
    }

    private void Start()
    {
        if (newSceneLoaded)
        {
            quests.Clear();
            orderList();
            quests.ForEach(x => { if (x.ID == currQuest && !x.SideQuest) x.enabled = true; });
            newSceneLoaded = false;
        }

        if (status.Equals(Status.Unavailable))
        {
            if (!SideQuest)
                currQuestStatus = status = ID == currQuest ? Status.Available : Status.Unavailable;
            else currQuestStatus = status = ID <= currQuest ? Status.Available : Status.Unavailable;
        }
        Trigger1QuestOnly(gameObject);
        GetComponent<BoxCollider2D>().enabled = enabled = status == Status.Available;
    }
    private void OnEnable()
    {
        if (status.Equals(Status.Unavailable))
        {
            if (!SideQuest)
                currQuestStatus = status = ID == currQuest ? Status.Available : Status.Unavailable;
            else currQuestStatus = status = ID <= currQuest ? Status.Available : Status.Unavailable;
        }
        print(currQuest);

        GetComponent<TalkScript>().dialogueEnglish = Dialogue;
        ActiveQuestSign.SetActive(true);
    }

    private void Update()
    {
        CheckNPCchanges();
        currTime += Time.deltaTime;
        if (NPCToReturnTo && type.Equals(Type.Return))
        {
            if (NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, colliders) > 1
                && status.Equals(Status.ReadyToComplete))
                startQuest = true;
            if (NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, colliders) == 1 && startQuest)
            {
                disableScript();
                ActiveQuestSign.SetActive(false);
                this.enabled = false;
            }
        }
        if (status.Equals(Status.OnGoing))
        {
            if (DeliverRequest) status = currQuestStatus = Status.ReadyToComplete;
            NPCToReturnTo.transform.GetChild(0).GetComponent<TalkScript>().dialogueEnglish = QuestCompleted;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPC_Quests = NPCToReturnTo.GetComponentsInChildren<NewQuest>().Where(x => x.status == Status.Available).ToList();
        NPC_Quests.ForEach(x => x.enabled = false);
        if (collision.CompareTag("Player") && status.Equals(Status.Available) && this.enabled)
        {
            GetComponent<TalkScript>().dialogueEnglish = Dialogue;
            status = currQuestStatus = Status.OnGoing;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Trigger1QuestOnly(gameObject);
    }
    private void CheckNPCchanges()
    {
        if (currTime > 5)
        {
            NPC_Quests = NPCToReturnTo.GetComponentsInChildren<NewQuest>().Where(x => x.status == Status.Available).ToList();
            NPC_Quests.ForEach(x => x.enabled = false);
            NPCToReturnTo.transform.GetChild(0).GetComponent<TalkScript>().dialogueEnglish = QuestCompleted;
            currTime = 0;
        }
    }

    public void disableScript()
    {
        GetComponent<TalkScript>().dialogueEnglish = null;
        if(NPCToReturnTo.transform.GetChild(0).GetComponent<TalkScript>().dialogueEnglish==QuestCompleted) NPCToReturnTo.transform.GetChild(0).GetComponent<TalkScript>().dialogueEnglish  = null;
        status = currQuestStatus = Status.Completed;
        if(!SideQuest) currQuest++;
        if(status==Status.Completed) EnableNextQuests();
    }


    private void EnableNextQuests()
    {
        quests.ForEach(x =>
        {
            if (x.ID != currQuest)
                x.enabled = x.GetComponent<BoxCollider2D>().enabled = x.NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        });

        quests.ForEach(x=>
        {
            if (x.ID == currQuest && !x.SideQuest || x.SideQuest && x.ID < currQuest && x.status!=Status.Completed)
                x.enabled = x.GetComponent<BoxCollider2D>().enabled = x.NPCToReturnTo.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        });
    }
}


