using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Created by Toby Wishart
 * Last edit: 19/10/19
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
                GameObject.Find("InventoryHotbar").GetComponent<Inventory>().addItem(ID, quantity, false);
                Destroy(gameObject);
            }
        }
    }
}
