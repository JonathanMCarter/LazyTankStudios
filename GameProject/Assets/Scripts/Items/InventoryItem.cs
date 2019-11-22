using UnityEngine;
public class InventoryItem : A
{
    public int ID;
    public int quantity;

    public void pickup()
    {
        F<SoundPlayer>().Play("Pick_Up_Item_1");
        Inventory inv = G<Inventory>(FT("Inv"));
        if (ID == 1024) inv.addCoins(quantity);
        else inv.items.Add(ID);
        D(gameObject);
    }
}