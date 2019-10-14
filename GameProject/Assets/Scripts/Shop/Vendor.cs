using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    /* 
     * Vendor Script
     * 
     * Created by: Gabriel Potamianos
     * Last edited: 14/10/2019
     * 
     * It does retrieve the file dialogue and connects it to the dialogue handler.
     * Opens the inventories

    */
    //File to get Dialgoue
    public DialogueFile VendorSpeech;

    //Inventories
    Inventory inventory;
    Inventory VendorInventory;

    //Do it once
    bool happened = false;

    //X position of the inventory 
    const int PANEL_POSITION_VENDOR_ON = 250;

    private void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        VendorInventory = GameObject.Find("VendorInventory").GetComponent<Inventory>();
    }


    private void Update()
    {
        if (GameObject.Find("DialogueHandler").GetComponent<DialogueScript>().FileHasEnded && !happened)
        {
            //Open inventories
            inventory.isOpen = true;
            VendorInventory.isOpen = true;
           // inventory.vendorMode = true;

            //this only happens once
            happened = !happened;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Resets itself everytime you get into the interactable zone
        happened = false;

        if (collision.gameObject.name == "Hero")
        {
            //Play speech
            GameObject.Find("DialogueHandler").GetComponent<DialogueScript>().ChangeFile(VendorSpeech);

            //Position the inventories
            inventory.ToogleVendorModeON(PANEL_POSITION_VENDOR_ON);
            VendorInventory.ToogleVendorModeON(-PANEL_POSITION_VENDOR_ON);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Reset the main inventory on leaving
        inventory.ToogleVendorModeON(-PANEL_POSITION_VENDOR_ON);
    }
}
