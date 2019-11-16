using UnityEngine;


/**
 * Created by Toby Wishart
 * Last edit: 05/11/19
 * Reason: Added gold pickup
 * 
 * Last Edit: Jonathan Carter
 * Last edit: 21/10/19
 * 
 * Made it so the player inv is ref correctly
 * 
 * An item that can be found in the world and picked up, the sprite of the item doesn't matter as the icon in the inventory is a separate icon
 * So for example, if there were hidden items, no sprite could be used
 * 
 * Also Edited By: Lewis Cleminson
 * Last Edit: 21.10.19
 * Reason: Updated controls to use Input Manager (Allows for cross platform input)
 */
public class InventoryItem : A
{
    InputManager IM;
    public int ID;
    public int quantity = 1;

    void Start()
    {
        IM = FindObjectOfType<InputManager>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Hero"))
        {
           // Debug.Log(collision.gameObject.name);

            if (IM.Button_B())
            {
                // Jonathan - Making sure the ref is always to the player inv
                for (int i = 0; i < FindObjectsOfType<Inventory>().Length; i++)
                {
                    //Toby: Only player inventoy has tag
                    if (GameObject.FindGameObjectWithTag("Inv"))
                    {
                        Inventory inv = GameObject.FindGameObjectWithTag("Inv").GetComponent<Inventory>();
                        //Special exception for gold
                        if (ID == 1024)inv.addCoins(quantity);
                        else inv.addItem(ID, quantity, false);

                        //GameObject.Find("InventoryHotbar").GetComponent<Inventory>().addItem(ID, quantity, false);
                        Destroy(gameObject);
                        break;
                    }
                }
            }
        }
    }
}
