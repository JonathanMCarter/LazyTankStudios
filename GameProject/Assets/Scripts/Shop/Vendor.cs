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
    InvSlot playerInventorySlot;
    InvSlot VendorInventorySlot;
    //Do it once
    bool happened = false;
    bool Sell = true;
    //X position of the inventory 
    const int PANEL_POSITION_VENDOR_ON = 250;

    private void Start()
    {
        //Retrieve inventories
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        VendorInventory = GameObject.Find("VendorInventory").GetComponent<Inventory>();

    }


    private void Update()
    {
        playerInventorySlot = inventory.transform.GetChild(inventory.selected).GetComponent<InvSlot>();
        VendorInventorySlot = VendorInventory.transform.GetChild(VendorInventory.selected).GetComponent<InvSlot>();
        //CHANGE IT
        if (GameObject.Find("DialogueHandler").GetComponent<DialogueScript>().FileHasEnded && !happened)
        {
            displaySellorBuyPanel(1);

            //this only happens once
            happened = !happened;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            VendorInventory.StartCoroutine(VendorInventory.ToogleSlots(Sell));
            Sell = !Sell;
            inventory.StartCoroutine(inventory.ToogleSlots(Sell));
        }

        if (Input.GetKeyDown(KeyCode.Return) && GameObject.Find("DialogueHandler").GetComponent<DialogueScript>().FileHasEnded)
        {
            if (playerInventorySlot.hasItem && Sell)
            {
                if (playerInventorySlot.quantity > 1)
                {
                    //Remove item from player inventory in relation with the quantity we have 
                    inventory.addItem(inventory.selected, playerInventorySlot.quantity, !Sell,1,!Sell);

                    //Add a item into the vendor inventory 
                    VendorInventory.addItem(VendorInventory.selected, VendorInventorySlot.quantity, !Sell,1,Sell);

                }
                else if (playerInventorySlot.quantity == 1)
                {
                    inventory.addItem(inventory.selected, playerInventorySlot.quantity, Sell);
                    VendorInventorySlot.quantity++;
                    VendorInventory.addItem(VendorInventory.selected, VendorInventorySlot.quantity, !Sell);
                }
            }
            else if (VendorInventorySlot.hasItem & !Sell)
            {
                if (VendorInventorySlot.quantity > 1)
                {

                    inventory.addItem(inventory.selected, playerInventorySlot.quantity, Sell,1,!Sell);
                    VendorInventory.addItem(VendorInventory.selected, VendorInventorySlot.quantity, Sell,1,Sell);

                }
                else if (VendorInventorySlot.quantity == 1)
                {
                    inventory.addItem(inventory.selected, playerInventorySlot.quantity, Sell,1,!Sell);
                    VendorInventory.addItem(VendorInventory.selected, VendorInventorySlot.quantity, !Sell,1,Sell);
                }
            }
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
            if(inventory.transform.localPosition.x>-PANEL_POSITION_VENDOR_ON)
                inventory.VendorPanelPosition(PANEL_POSITION_VENDOR_ON);
            
            if (VendorInventory.transform.localPosition.x < PANEL_POSITION_VENDOR_ON)
                VendorInventory.VendorPanelPosition(-PANEL_POSITION_VENDOR_ON);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Reset the main inventory on leaving
        inventory.VendorPanelPosition(-PANEL_POSITION_VENDOR_ON);
    }

    public void SellOrBuy(bool action)
    {
        Sell = action;
        if (Sell)
            VendorInventory.StartCoroutine(VendorInventory.ToogleSlots(!Sell));
        else inventory.StartCoroutine(inventory.ToogleSlots(Sell));
        print(Sell);


        inventory.open();
        inventory.VendorMode = true;
        VendorInventory.open();
        VendorInventory.VendorMode = true;
        displaySellorBuyPanel(0);

    }
    void displaySellorBuyPanel(int state)
    {
        GameObject.Find("SellOrBuyPanel").GetComponent<CanvasGroup>().alpha = state;
    }
}
