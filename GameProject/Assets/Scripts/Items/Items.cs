using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * Created by Toby Wishart
 * Last edit: 12/10/19
 * 
 * Single class to handle using items using a simple switch statement
 */
public class Items : MonoBehaviour
{
    //Enum indexes should match the index of the inventory slots, so this should contain every item
    enum ITEMS
    {
        SWORD, AXE, BOOK, STAFF
    }

    Inventory i;

    void Start()
    {
        i = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        //input to use item
        if (Input.GetButton("Submit") && !i.isOpen)
        {
            switch (i.equipped)
            {
                case 0:
                    //do something when using sword here
                    Debug.Log("Using sword");
                    break;

                case -1:
                default:
                    //nothing or invalid item equipped
                    Debug.Log("Trying to use nothing");
                    break;
            }
        }
    }
}
