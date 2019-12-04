using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Quest: A {
 static public bool[] boss = new bool[8];
 public int ID;
 public enum Type {
  Return,
  NonReturn
 };
 public Type type;
 public bool SideQuest;
 public GameObject NPCToReturnTo;
 public DialogueFile Dialogue;
 public DialogueFile QuestCompleted;
 public bool KillRequest;
 public bool CollectRequest;
 public bool DeliverRequest;
 public List < GameObject > Kills = new List < GameObject > ();
 public Sprite[] ItemsToBeCollected;
 public int[] ItemsQuantities;
 public int DeliverGold;
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
 static public int currQuest = 0;
 static GameObject currQuestGO;
 static Status currQuestStatus;
 GameObject ActiveQuestSign;
 PlayerMovement HeroRef;
 DialogueScript ds;
 ContactFilter2D contactFilter;
 void InitialiseQuests(Status StatusToSet) {
  status = ID == currQuest ? StatusToSet : Status.NotAvailable;
  if (gameObject == GetCurrentQuestGameObject()) {
   G<BoxCollider2D>().enabled = G<BoxCollider2D>().enabled = true;
   ActiveQuestSign.SetActive(status == StatusToSet);
  } else {
   G<BoxCollider2D>().enabled = status == StatusToSet ? true : false;
   G<BoxCollider2D>(C(NPCToReturnTo.transform,0).gameObject).enabled = status == StatusToSet ? true : false;
  }
  enabled = status == StatusToSet;
 }
 GameObject GetCurrentQuestGameObject() {
  foreach(Quest q in quests) if (q.ID == currQuest) return q.gameObject;
  return null;
 }
 void Awake() {
  ds = F<DialogueScript>();
  HeroRef = F<PlayerMovement>();
  ActiveQuestSign = C(gameObject.transform.parent,1).gameObject;
  newItem = Resources.Load<GameObject>("New Item");
  inv = F<Inventory>();
  quests = Fs<Quest>();
  GameManagerTalk = G<TalkScript>(F("GameManager"));
  InitialiseQuests(Status.Available);
 }
 void Start() {
  if (currQuestStatus > Status.Completed) InitialiseQuests(currQuestStatus);
 }
 void Update() {
  //print(currQuest + " STATUS: " + currQuestStatus);
  if (NPCToReturnTo && type.Equals(Type.Return)) {
   if (G<BoxCollider2D>(C(NPCToReturnTo.transform,0).gameObject).OverlapCollider(contactFilter, colliders) > 1 && status.Equals(Status.ReadyToComplete)) {
    displayQuestCompletedDialogue();
    findNextQuest();
   }
  } else if (status.Equals(Status.ReadyToComplete)) {
   displayQuestCompletedDialogue();
   findNextQuest();
  }
  if (status == Status.OnGoing)
   if (checkKilledAllEnemies() && KillRequest || checkItemsCollected() && CollectRequest || DeliverRequest && inv.getCoins() >= DeliverGold) {
    status = currQuestStatus = Status.ReadyToComplete;
    if (type == Type.Return) G<BoxCollider2D>(C(NPCToReturnTo.transform,0).gameObject).enabled = true;
   }
 }
 void OnTriggerEnter2D(Collider2D collision) {
  if (status != Status.ReadyToComplete && ID == currQuest || status != Status.ReadyToComplete && ID <= currQuest && SideQuest) {
   if (collision.gameObject.name == "Hero" && isActiveAndEnabled) {
    G<TalkScript>().dialogueEnglish = Dialogue;
    if (DeliverRequest && status == Status.Available) inv.addCoins(DeliverGold);
    status = currQuestStatus = Status.OnGoing;
   }
  }
 }
 bool checkKilledAllEnemies() {
  bool state = false;
        if (Kills.Count > 0) Kills.ForEach(enemy=>state=enemy.activeInHierarchy?false:true);
        else{
            state = boss[NewAIMove.currBoss] == true;
            if (state) NewAIMove.currBoss++;
        }
  return state;
 }
 bool checkItemsCollected() {
  bool state = false;
  foreach(int elementQuantity in ItemsQuantities) state = elementQuantity == 0 ? true : false;
  return state;
 }
 void displayQuestCompletedDialogue() {
  if (DeliverRequest) {
   if (inv.getCoins() >= DeliverGold) {
    inv.addCoins(-DeliverGold);
    GameManagerTalk.dialogueEnglish = QuestCompleted;
    GameManagerTalk.talk();
    offerReward(reward);
   }
  } else {
   GameManagerTalk.dialogueEnglish = QuestCompleted;
   GameManagerTalk.talk();
   offerReward(reward);
  }
  status = currQuestStatus = Status.Completed;
  currQuest++;
 }
 public void findNextQuest() {
  bool lastQuest = true;
  foreach(Quest q in quests) {
   if (q.ID == ID + 1 && !SideQuest) {
    q.enabled = G<BoxCollider2D>(q.gameObject).enabled = true;
    enabled = false;
    if (q.gameObject != this.gameObject) lastQuest = false;
    q.ActiveQuestSign.SetActive(true);
    ActiveQuestSign.SetActive(false);
    q.status = currQuestStatus = Status.Available;
   } else if (SideQuest && currQuest >= q.ID && q.status != Status.Completed) {
    enabled = G<BoxCollider2D>().enabled = false;
    ActiveQuestSign.SetActive(false);
    q.status = Status.Available;
    lastQuest = false;
   }
  }
  if (lastQuest) {
   enabled = G<BoxCollider2D>().enabled = G<BoxCollider2D>(C(NPCToReturnTo.transform,0).gameObject).enabled = false;
   ActiveQuestSign.SetActive(false);
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