using UnityEngine;
public class Vendor: A {
public GameObject p;
public int price;
bool happened = true;
CanvasGroup cg;
InputManager IM;
Inventory inv;
void Start(){
cg = G<CanvasGroup>(F("VendorInventory"));
IM = F<InputManager>();
inv = G<Inventory>(F("PlayerInventory"));}
void Update(){
if (F<DialogueScript>().FileHasEnded && !happened){
cg.alpha = 1;
happened = !happened;}
else if (happened && IM.Button_Menu())
cg.alpha = 0;
G<PlayerMovement>(F("Hero")).enabled = cg.alpha == 1 ? false : true;
if (IM.Button_A() && cg.alpha == 1 && inv.getCoins() >= price){
if (p.GetComponent<InventoryItem>().ID == 4) inv.AddPotion();
else inv.items.Add(p.GetComponent<InventoryItem>().ID);
F<SoundPlayer>().Play("Pick_Up_Item_1");
inv.addCoins(-price);}}
void OnTriggerEnter2D(Collider2D collision){
if(collision.CompareTag("Player"))happened = false;}}