using UnityEngine;


public class InventoryItem : A
{
    InputManager IM;
    public int ID;
    public int quantity;
    void Start()
    {
        IM = FindObjectOfType<InputManager>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Hero"))
        {

            if (IM.Button_B())
            {
                        Inventory inv = GameObject.FindGameObjectWithTag("Inv").GetComponent<Inventory>();
                        
                        if (ID == 1024)inv.addCoins(quantity);
                        else inv.addItem(ID, 0, false, true);
                        Destroy(gameObject);
                        
            }
        }
    }
}
