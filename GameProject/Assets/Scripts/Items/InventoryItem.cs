public class InventoryItem : A{
public int ID;
public int quantity;
public bool Unique;
public void pickup(){
F<SoundPlayer>().Play("Pick_Up_Item_1");
Inventory inv = G<Inventory>(FT("Inv"));
if (ID == 1024) inv.addCoins(quantity);
else if (ID == 4) inv.AddPotion();
else{
if (!Unique) inv.items.Add(ID);
else if (!inv.items.Contains(ID)) inv.items.Add(ID);}
D(gameObject);}}