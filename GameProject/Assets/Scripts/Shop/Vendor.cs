using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor : MonoBehaviour
{
    /* 
     * Vendor Script
     * 
     * Created by: Gabriel Potamianos
     * Last Edited by: Jonathan Carter @ some ungodly hour....
     * Last edited: 19/10/2019
     * 
     * It does retrieve the file dialogue and connects it to the dialogue handler.
     * Opens the inventories

    */
    //File to get Dialgoue
    public DialogueFile VendorSpeech;

    //Inventories
    public Inventory inventory;
    public Inventory VendorInventory;
    public InvSlot playerInventorySlot;
    public InvSlot VendorInventorySlot;

    //Do it once
    bool happened = false;
    bool Sell = true;
    int selected = 0;

    //X position of the inventory 
    const int PANEL_POSITION_VENDOR_ON = 250;

    // Jonathan Edit
    private DialogueScript DS;
    public bool IsUsingVendor;

    private void Start()
    {
        // Jonathan Edit
        inventory = GameObject.Find("SellOrBuyPanel").transform.GetChild(2).GetComponent<Inventory>();
        VendorInventory = GameObject.Find("SellOrBuyPanel").transform.GetChild(3).GetComponent<Inventory>();

        if (GameObject.Find("SellOrBuyPanel").transform.GetChild(0).gameObject.activeInHierarchy)
        {
            GameObject.Find("SellOrBuyPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("SellOrBuyPanel").transform.GetChild(1).gameObject.SetActive(false);
        }

        DS = FindObjectOfType<DialogueScript>();

        //Retrieve inventories
        //inventory = GameObject.Find("PlayerInventory").GetComponent<Inventory>();
        //VendorInventory = GameObject.Find("VendorInventory").GetComponent<Inventory>();

    }


    private void Update()
    {
        if (!inventory.gameObject.activeInHierarchy)
        {
            inventory.gameObject.SetActive(true);
        }

        if (!VendorInventory.gameObject.activeInHierarchy)
        {
            VendorInventory.gameObject.SetActive(true);
        }

        playerInventorySlot = inventory.transform.GetChild(inventory.selected).GetComponent<InvSlot>();
        VendorInventorySlot = VendorInventory.transform.GetChild(VendorInventory.selected).GetComponent<InvSlot>();
        StartCoroutine(delay());

        if (isSellOrBuyPanelOpened())
        {
            GameObject.Find("SellOrBuyPanel").transform.GetChild(selected).GetComponent<Image>().color = new Color(0.745283f, 0.745283f, 0.745283f);

            if (Input.GetAxisRaw("Vertical") == 1 && selected < 2 && selected > 0)
            {
                StartCoroutine(delay());
                selected--;

            }
            else if (Input.GetAxisRaw("Vertical") == -1 && selected >= 0 && selected < 1)
            {
                StartCoroutine(delay());
                selected++;

            }
            GameObject.Find("SellOrBuyPanel").transform.GetChild(selected).GetComponent<Image>().color = new Color(1, 1, 1);

        }

        if (IsUsingVendor)
        {
            if (Input.GetKeyDown(KeyCode.Return) && inventory.isOpen)
            {
                if (playerInventorySlot.hasItem && Sell)
                {
                    if (playerInventorySlot.quantity > 1)
                    {
                        //Remove item from player inventory in relation with the quantity we have 
                        inventory.addItem(inventory.selected, playerInventorySlot.quantity, !Sell, 1, !Sell);

                        //Add a item into the vendor inventory 
                        VendorInventory.addItem(VendorInventory.selected, VendorInventorySlot.quantity, !Sell, 1, Sell);
                    }
                    else if (playerInventorySlot.quantity == 1)
                    {
                        inventory.addItem(inventory.selected, playerInventorySlot.quantity, Sell, 1, !Sell);
                        VendorInventory.addItem(VendorInventory.selected, VendorInventorySlot.quantity, !Sell, 1, Sell);
                    }
                }
                else if (VendorInventorySlot.hasItem & !Sell)
                {
                    if (VendorInventorySlot.quantity > 1)
                    {
                        inventory.addItem(inventory.selected, playerInventorySlot.quantity, Sell, 1, !Sell);
                        VendorInventory.addItem(VendorInventory.selected, VendorInventorySlot.quantity, Sell, 1, Sell);
                    }
                    else if (VendorInventorySlot.quantity == 1)
                    {
                        inventory.addItem(inventory.selected, playerInventorySlot.quantity, Sell, 1, !Sell);
                        VendorInventory.addItem(VendorInventory.selected, VendorInventorySlot.quantity, !Sell, 1, Sell);
                    }
                }
            }
        }

        // Check to see if the file been read is actually the one on the vendor.....
        // Shouldn't cause problems anymore......
        if (DS.File == VendorSpeech)
        {
            if (DS.FileHasEnded && !happened)
            {
                IsUsingVendor = true;

                // Jonathan Edit - Makes it so it enables the sell / buy buttons again after they are disabled the first time
                if (!GameObject.Find("SellOrBuyPanel").transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    GameObject.Find("SellOrBuyPanel").transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("SellOrBuyPanel").transform.GetChild(1).gameObject.SetActive(true);
                }

                VendorInventory.gameObject.SetActive(false);

                ToogleSellorBuyPanel(1);

                if (isSellOrBuyPanelOpened())
                {
                    GameObject.Find("Hero").GetComponent<PlayerMovement>().enabled = !isSellOrBuyPanelOpened();
                }


                if (Input.GetKeyDown(KeyCode.Return))
                {
                    switch (selected)
                    {
                        case 0:
                            SellOrBuy(true);
                            break;
                        case 1:
                            SellOrBuy(false);
                            break;
                    }

                    //this only happens once
                    happened = !happened;

                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            VendorInventory.StartCoroutine(VendorInventory.ToogleSlots(Sell));
            Sell = !Sell;
            inventory.StartCoroutine(inventory.ToogleSlots(Sell));
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Resets itself everytime you get into the interactable zone
        happened = false;
        
        if (collision.gameObject.name == "Hero")
        {
            //Play speech - edited by jonathan to use findoftype
            DS.ChangeFile(VendorSpeech);

            //Position the inventories
            if (inventory.transform.localPosition.x>-PANEL_POSITION_VENDOR_ON)
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
        {
            VendorInventory.StartCoroutine(VendorInventory.ToogleSlots(!Sell));
            inventory.StartCoroutine(inventory.ToogleSlots(Sell));
        }
        else
        {
            inventory.StartCoroutine(inventory.ToogleSlots(Sell));
            VendorInventory.StartCoroutine(VendorInventory.ToogleSlots(!Sell));
        }


        inventory.open();
        inventory.VendorMode = true;
        VendorInventory.gameObject.SetActive(true);
        VendorInventory.open();
        VendorInventory.VendorMode = true;

        // Jonathan Edit - Makes it so it disables the sell / buy buttons
        GameObject.Find("SellOrBuyPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("SellOrBuyPanel").transform.GetChild(1).gameObject.SetActive(false);
        IsUsingVendor = false;

        //ToogleSellorBuyPanel(0);

    }

    void ToogleSellorBuyPanel(int state)
    {
        GameObject.Find("SellOrBuyPanel").GetComponent<CanvasGroup>().alpha = state;
    }

    bool isSellOrBuyPanelOpened()
    {
        return GameObject.Find("SellOrBuyPanel").GetComponent<CanvasGroup>().alpha == 1 ? true : false;

    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.1f);
    }



}
