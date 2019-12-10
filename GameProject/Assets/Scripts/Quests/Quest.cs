using System.Collections;
using UnityEngine;
public class Quest: A {
static public bool[] boss = new bool[8];
public bool[] bosses;
public int ID;
[TextArea]
public string QuestTag;
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
public int killsNeeded;
public Sprite[] ItemsToBeCollected;
public int ItemToCollect;
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
public GameObject[] rewardItemsNew;
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
public static int currQuest = 0;
static GameObject currQuestGO;
static Status currQuestStatus;
GameObject ActiveQuestSign;
PlayerMovement HeroRef;
DialogueScript ds;
public ContactFilter2D contactFilter;
GameObject RAQS;
BoxCollider2D RBC;
QuestLog log;
void UpdateLog(){
log.SaveQuest(ID, QuestTag, status, ItemToCollect, killsNeeded);}
void InitialiseQuests(Status StatusToSet) {
if (status != Status.Completed) status = ID == currQuest ? StatusToSet : Status.NotAvailable;
if (SideQuest && status != Status.Completed) status = ID <= currQuest ? StatusToSet : Status.NotAvailable;
if (gameObject == GetCurrentQuestGameObject()) {
G<BoxCollider2D>().enabled = true;}
else {
G<BoxCollider2D>().enabled = status == StatusToSet ? true : false;
G<BoxCollider2D>(C(NPCToReturnTo.transform,0).gameObject).enabled = status == StatusToSet ? true : false;}
enabled = (status != Status.NotAvailable && status != Status.Completed);}
GameObject GetCurrentQuestGameObject() {
foreach(Quest q in quests) if (q.ID == currQuest) return q.gameObject;
return null;}
void Awake() {
ds = F<DialogueScript>();
HeroRef = F<PlayerMovement>();
ActiveQuestSign = C(gameObject.transform.parent,1).gameObject;
RAQS = NPCToReturnTo.transform.GetChild(1).gameObject;
RBC = G<BoxCollider2D>(C(NPCToReturnTo.transform, 0).gameObject);
ActiveQuestSign.SetActive(false);
newItem = Resources.Load<GameObject>("New Item");
inv = F<Inventory>();
quests = Fs<Quest>();
GameManagerTalk = G<TalkScript>(F("GameManager"));
InitialiseQuests(Status.Available);
log = FindObjectOfType<QuestLog>();}
void Start() {
StartCoroutine(latestart());}
IEnumerator latestart(){
yield return new WaitForSeconds(0);
if (currQuestStatus > Status.Completed) InitialiseQuests(currQuestStatus);
yield return new WaitForSeconds(0);
if (type == Type.Return && status == Status.ReadyToComplete){
G<BoxCollider2D>(C(NPCToReturnTo.transform, 0).gameObject).enabled = true;}
if (status != Status.Completed && status != Status.NotAvailable){
ActiveQuestSign.SetActive(true);
if (status != Status.Available) RAQS.SetActive(true);}}
void Update() {
bosses = boss;
if (NPCToReturnTo && type.Equals(Type.Return)) {
if (RBC.OverlapCollider(contactFilter, colliders) > 1 && status.Equals(Status.ReadyToComplete)) {
displayQuestCompletedDialogue();
findNextQuest();}}
else if (status.Equals(Status.ReadyToComplete)) {
displayQuestCompletedDialogue();
findNextQuest();}
if (status == Status.OnGoing)
if (checkKilledAllEnemies() && KillRequest || checkItemsCollected() && CollectRequest || DeliverRequest && inv.getCoins() >= DeliverGold) {
status = currQuestStatus = Status.ReadyToComplete;
UpdateLog();
if (type == Type.Return) {
G<BoxCollider2D>(C(NPCToReturnTo.transform, 0).gameObject).enabled = true;}}
if (status == Status.Completed && enabled && SideQuest) findNextQuest();}
void OnTriggerEnter2D(Collider2D collision) {
if ( ID == currQuest || ID <= currQuest && SideQuest){
if (collision.gameObject.name == "Hero" && isActiveAndEnabled) {
G<TalkScript>().dialogueEnglish = Dialogue;
G<TalkScript>().dialogueGerman = GerDial;
if (DeliverRequest && status == Status.Available) inv.addCoins(DeliverGold);
status = currQuestStatus = Status.OnGoing;
RAQS.SetActive(true);
UpdateLog();}}}
bool checkKilledAllEnemies() {
bool state = false;
if (BossQuest)state = boss[NewAIMove.currBoss] == true;
else{
state = (killsNeeded <= 0);}
return state;}
bool checkItemsCollected() {
return (ItemToCollect <= 0);}
void displayQuestCompletedDialogue() {
ActiveQuestSign.SetActive(false);
RAQS.SetActive(false);
if (DeliverRequest) {
if (inv.getCoins() >= DeliverGold) {
inv.addCoins(-DeliverGold);
GameManagerTalk.dialogueEnglish = QuestCompleted;
GameManagerTalk.dialogueGerman = GerQComp;
GameManagerTalk.talk();
offerReward(reward);}} else {
GameManagerTalk.dialogueEnglish = QuestCompleted;
GameManagerTalk.dialogueGerman = GerQComp;
GameManagerTalk.talk();
offerReward(reward);}
status = currQuestStatus = Status.Completed;
UpdateLog();
if (currQuest == ID && !SideQuest) currQuest++;
print(currQuest);
if (BossQuest) { NewAIMove.currBoss++; print("Current Boss: " + NewAIMove.currBoss); }}
public void findNextQuest() {
FindObjectOfType<QuestLog>().SetQuest();
bool lastQuest = true;
if (SideQuest) enabled = G<BoxCollider2D>().enabled = G<BoxCollider2D>(C(NPCToReturnTo.transform, 0).gameObject).enabled = false;
if (!SideQuest){
foreach (Quest q in quests){
if (q.ID == ID + 1 && !q.SideQuest){
q.enabled = G<BoxCollider2D>(q.gameObject).enabled = true;
q.ActiveQuestSign.SetActive(true);
enabled = false;
if (q.gameObject != this.gameObject) lastQuest = false;
q.status = currQuestStatus = Status.Available;}
else if (q.SideQuest && currQuest >= q.ID && q.status != Status.Completed){
q.enabled = G<BoxCollider2D>().enabled = true;
q.ActiveQuestSign.SetActive(true);
q.status = Status.Available;}}
if (lastQuest){
enabled = G<BoxCollider2D>().enabled = G<BoxCollider2D>(C(NPCToReturnTo.transform, 0).gameObject).enabled = false;
ActiveQuestSign.SetActive(false);}}}
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
break;}}
void throwItems() {
Vector3 updatedPosition = type == Type.Return ? NPCToReturnTo.transform.position : F("Hero").transform.position;
for (int i = 0; i < rewardItems.Length; i++) {
GameObject temp = Instantiate(rewardItemsNew[i], updatedPosition, Quaternion.identity);
G<SpriteRenderer>(temp).sprite = rewardItems[i];
G<SpriteRenderer>(temp).sortingOrder = 20;
temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
if ((int) rewardIdsAndQuantities[i].x >= 0 && (int) rewardIdsAndQuantities[i].x < 12) G<InventoryItem>(temp).ID = (int) rewardIdsAndQuantities[i].x;
if ((int) rewardIdsAndQuantities[i].y > 0) G<InventoryItem>(temp).quantity = (int) rewardIdsAndQuantities[i].y;
temp.AddComponent < Rigidbody2D > ();
G<Rigidbody2D>(temp).AddForce(new Vector2(Random.Range(-150, 150), Random.Range(-150, 150)));
SC(freezeItemGravity(G<Rigidbody2D>(temp)));}}
IEnumerator freezeItemGravity(Rigidbody2D temporaryObject) {
yield return new WaitForSeconds(0.5f);
temporaryObject.gravityScale = 0;
temporaryObject.bodyType = RigidbodyType2D.Static;}}