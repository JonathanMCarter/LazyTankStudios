using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Created by Toby Wishart
 * 
 * Last Edit: Jonathan Carter
 * Last edit: 21/10/19
 * 
 * Made it so the player inv is ref correctly
 * 
 * An item that can be found in the world and picked up, the sprite of the item doesn't matter as the icon in the inventory is a separate icon
 * So for example, if there were hidden items, no sprite could be used
 */
public class InventoryItem : MonoBehaviour
{
    public int ID;
    public int quantity = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Hero"))
        {
           // Debug.Log(collision.gameObject.name);

            if (Input.GetButton("Submit"))
            {
                // Jonathan - Making sure the ref is always to the player inv
                for (int i = 0; i < FindObjectsOfType<Inventory>().Length; i++)
                {
                    if (FindObjectsOfType<Inventory>()[i].gameObject.name.Contains("Player"))
                    {
                        FindObjectsOfType<Inventory>()[1].addItem(ID, quantity, false);
                        //GameObject.Find("InventoryHotbar").GetComponent<Inventory>().addItem(ID, quantity, false);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
