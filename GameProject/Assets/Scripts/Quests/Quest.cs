using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Quest: A {
 static public bool[] boss = new bool[8];
    public bool[] bosses;
 public int ID;
    [TextArea]//temp add by LC to help organise inspector
    public string QuestTag; //temp add by LC to help organise inspector
 public enum Type {
  Return,
  NonReturn
 };
 public Type type;
 public bool SideQuest;
 public GameObject NPCToReturnTo;
 public DialogueFile Dialogue, QuestCompleted, GerDial, GerQComp;
 public bool KillRequest;
 public bool CollectRequest;
 public bool DeliverRequest;
 public List < GameObject > Kills = new List < GameObject > ();
 public Sprite[] ItemsToBeCollected;
 public int[] ItemsQuantities;
 public int DeliverGold;
    public bool BossQuest;
 public enum Reward {
  Items,
  Gold,
  GoldAndItems
 }
 public Reward reward;
 public Sprite[] rewardItems;
    public GameObject[] rewardItemsNew; //temp add by LC
 GameObject newItem;
 public Vector2[] rewardIdsAndQuantities;
 public int rewardGold;
 public enum Status {
  NotAvailable,
  Completed,
  Available,
  OnGoing,
  ReadyToComplete
 };
    public Status status;
 TalkScript GameManagerTalk;
 Collider2D[] colliders = new Collider2D[10];
 Quest[] quests;
 Inventory inv;
 public static  int currQuest = 0;
 static GameObject currQuestGO;
 static Status currQuestStatus;
 GameObject ActiveQuestSign;
 PlayerMovement HeroRef;
 DialogueScript ds;
 ContactFilter2D contactFilter;
    GameObject RAQS; // Return Active Quest Sign
    QuestLog log;


    void UpdateLog() //temp added by LC
    {
        log.SaveQuest(ID, QuestTag, status);
    }

 void InitialiseQuests(Status StatusToSet) { //how does this work for side quests?
        if (status != Status.Completed) status = ID == currQuest ? StatusToSet : Status.NotAvailable;
        if (SideQuest && status != Status.Completed) status = ID <= currQuest ? StatusToSet : Status.NotAvailable; //check added by LC for side quests
        if (gameObject == GetCurrentQuestGameObject()) {
        //G<BoxCollider2D>().enabled = G<BoxCollider2D>().enabled = true;
            G<BoxCollider2D>().enabled = true; //changed by LC
            //ActiveQuestSign.SetActive(status == StatusToSet);
            //ActiveQuestSign.SetActive(true); //changed by LC
        }
        else {
            G<BoxCollider2D>().enabled = status == StatusToSet ? true : false;
            G<BoxCollider2D>(C(NPCToReturnTo.transform,0).gameObject).enabled = status == StatusToSet ? true : false;
        }
    enabled = (status != Status.NotAvailable && status != Status.Completed);
 }


 GameObject GetCurrentQuestGameObject() {
  foreach(Quest q in quests) if (q.ID == currQuest) return q.gameObject;
  return null;
 }


 void Awake() {
  ds = F<DialogueScript>();
  HeroRef = F<PlayerMovement>();
  ActiveQuestSign = C(gameObject.transform.parent,1).gameObject;
        RAQS = NPCToReturnTo.transform.GetChild(1).gameObject; //late add by LC

ActiveQuestSign.SetActive(false); //Late add by LC


  newItem = Resources.Load<GameObject>("New Item");
  inv = F<Inventory>();
  quests = Fs<Quest>();
  GameManagerTalk = G<TalkScript>(F("GameManager"));
  InitialiseQuests(Status.Available);
        log = FindObjectOfType<QuestLog>(); //temp add by LC
 }
 void Start() {
 
        StartCoroutine(latestart());
       
 }

    IEnumerator latestart() //temp add by LC
    {
        yield return new WaitForSeconds(0);
        //FindObjectOfType<QuestLog>().SetQuest();
        if (currQuestStatus > Status.Completed) InitialiseQuests(currQuestStatus); //all quests start as not avaliable - how does this code run
        //if (SideQuest) InitialiseQuests(currQuestStatus);
        yield return new WaitForSeconds(0);
        if (type == Type.Return && status == Status.ReadyToComplete)
        {
            G<BoxCollider2D>(C(NPCToReturnTo.transform, 0).gameObject).enabled = true;
        }

        if (status != Status.Completed && status != Status.NotAvailable)
        {
           // print(transform.parent.gameObject.name);
            ActiveQuestSign.SetActive(true);
            if (status != Status.Available) RAQS.SetActive(true); //late add by LC 
            //NPCToReturnTo.transform.GetChild(1).gameObject.SetActive(true);//late add by LC
        }//late add by LC

    }




    void Update() { //this should prob not be every frame - may move to an ienumerator and cycle every few seconds - LC
                    //print(currQuest + " STATUS: " + currQuestStatus);
        bosses = boss;
    if (NPCToReturnTo && type.Equals(Type.Return)) {
            //this allows for everything to collide with the end npc, may be fixed with proper use of layers - LC
        if (G<BoxCollider2D>(C(NPCToReturnTo.transform,0).gameObject).OverlapCollider(contactFilter, colliders) > 1 && status.Equals(Status.ReadyToComplete)) { 
        displayQuestCompletedDialogue();
        findNextQuest();
        }
    }
    else if (status.Equals(Status.ReadyToComplete)) {
            displayQuestCompletedDialogue();
        findNextQuest();
    }

  if (status == Status.OnGoing)
    if (checkKilledAllEnemies() && KillRequest || checkItemsCollected() && CollectRequest || DeliverRequest && inv.getCoins() >= DeliverGold) {
    status = currQuestStatus = Status.ReadyToComplete;
                UpdateLog();
                if (type == Type.Return) {
                    G<BoxCollider2D>(C(NPCToReturnTo.transform, 0).gameObject).enabled = true;
                }
   }

        if (status == Status.Completed && enabled && SideQuest) findNextQuest(); //added by LC to stop side quests repeating
 }
 void OnTriggerEnter2D(Collider2D collision) {
        //if (status != Status.ReadyToComplete && ID == currQuest || status != Status.ReadyToComplete && ID <= currQuest && SideQuest) {
        if ( ID == currQuest ||  ID <= currQuest && SideQuest){
            if (collision.gameObject.name == "Hero" && isActiveAndEnabled) {
    G<TalkScript>().dialogueEnglish = Dialogue; //need to add in german text
                G<TalkScript>().dialogueGerman = GerDial;
    if (DeliverRequest && status == Status.Available) inv.addCoins(DeliverGold);
    status = currQuestStatus = Status.OnGoing;
               RAQS.SetActive(true);//late add by LC
                UpdateLog();
                //collision.gameObject.GetComponent<PlayerMovement>().myquests.Add(this);//temp add by LC. didnt work as intended
                //FindObjectOfType<QuestLog>().SaveQuest(ID, QuestTag, status);
            }
  }
 }
 bool checkKilledAllEnemies() {
  bool state = false;
        if (Kills.Count > 0) Kills.ForEach(enemy=>state=enemy.activeInHierarchy?false:true);
        else{
            state = boss[NewAIMove.currBoss] == true;
          
            //if (state) NewAIMove.currBoss++; //taken out by LC for changing scenes. increases at end of quest
        }
  return state;
 }
 bool checkItemsCollected() {
  bool state = false;
  foreach(int elementQuantity in ItemsQuantities) state = elementQuantity == 0 ? true : false;
  return state;
 }
 void displayQuestCompletedDialogue() {

        ActiveQuestSign.SetActive(false);//late add by LC
        RAQS.SetActive(false);


  if (DeliverRequest) {
   if (inv.getCoins() >= DeliverGold) {
    inv.addCoins(-DeliverGold);
    GameManagerTalk.dialogueEnglish = QuestCompleted;
                GameManagerTalk.dialogueGerman = GerQComp;
    GameManagerTalk.talk();
    offerReward(reward);
   }
  } else { ///////////////////some repeat code here, could be reduced?
   GameManagerTalk.dialogueEnglish = QuestCompleted;
            GameManagerTalk.dialogueGerman = GerQComp;
            GameManagerTalk.talk();
   offerReward(reward);
  }
  status = currQuestStatus = Status.Completed;
        UpdateLog();
        if (currQuest == ID && !SideQuest) currQuest++; //check added by LC as skipped a q, and increased with side quests
        print(currQuest);
        if (BossQuest) { NewAIMove.currBoss++; print("Current Boss: " + NewAIMove.currBoss); } //temp add by LC

 }
 public void findNextQuest() {
        FindObjectOfType<QuestLog>().SetQuest();
  bool lastQuest = true;
        if (SideQuest) enabled = G<BoxCollider2D>().enabled = G<BoxCollider2D>(C(NPCToReturnTo.transform, 0).gameObject).enabled = false;
        // if (NPCToReturnTo != this.gameObject) GetComponent<BoxCollider2D>().enabled = false; //temp added by LC
        if (!SideQuest)
        {
            foreach (Quest q in quests)
            {

                if (q.ID == ID + 1 && !q.SideQuest)
                {

                    q.enabled = G<BoxCollider2D>(q.gameObject).enabled = true;
                    q.ActiveQuestSign.SetActive(true);
                    enabled = false;
                    if (q.gameObject != this.gameObject) lastQuest = false;
                    //ActiveQuestSign.SetActive(false); //order swapped by LC
                    //q.ActiveQuestSign.SetActive(true);


                    q.status = currQuestStatus = Status.Available;
                }
                else if (q.SideQuest && currQuest >= q.ID && q.status != Status.Completed)
                { //edited late by LC
                    q.enabled = G<BoxCollider2D>().enabled = true;
                    q.ActiveQuestSign.SetActive(true);
                    q.status = Status.Available;

                    //lastQuest = false;

                }
            }
            if (lastQuest)
            {
                enabled = G<BoxCollider2D>().enabled = G<BoxCollider2D>(C(NPCToReturnTo.transform, 0).gameObject).enabled = false;
                ActiveQuestSign.SetActive(false);
            }
        }
 }
 public void offerReward(Quest.Reward typeOfReward) {
  Random.InitState(Random.Range(0, 100));
  switch (typeOfReward) {
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
 void throwItems() { //should change this to spawn prefab of object ratehr than creating new and setting up details of it ect. would work slightly better. Comment added by LC
  Vector3 updatedPosition = type == Type.Return ? NPCToReturnTo.transform.position : F("Hero").transform.position;
  for (int i = 0; i < rewardItems.Length; i++) {
   GameObject temp = Instantiate(rewardItemsNew[i], updatedPosition, Quaternion.identity); //changed to spawn RewardItemsNew rather than RewardItems to spawn a prefab - temp - LC
   G<SpriteRenderer>(temp).sprite = rewardItems[i];
   G<SpriteRenderer>(temp).sortingOrder = 20;
   temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
   if ((int) rewardIdsAndQuantities[i].x >= 0 && (int) rewardIdsAndQuantities[i].x < 12) G<InventoryItem>(temp).ID = (int) rewardIdsAndQuantities[i].x;
   if ((int) rewardIdsAndQuantities[i].y > 0) G<InventoryItem>(temp).quantity = (int) rewardIdsAndQuantities[i].y;
   temp.AddComponent < Rigidbody2D > ();
   G<Rigidbody2D>(temp).AddForce(new Vector2(Random.Range(-150, 150), Random.Range(-150, 150)));
   SC(freezeItemGravity(G<Rigidbody2D>(temp)));
  }
 }
 IEnumerator freezeItemGravity(Rigidbody2D temporaryObject) {
  yield
  return new WaitForSeconds(0.5f);
  temporaryObject.gravityScale = 0;
  temporaryObject.bodyType = RigidbodyType2D.Static;
 }
}