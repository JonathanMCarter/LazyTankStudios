//using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor : A
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

        Also Edited by: Lewis Cleminson
        Last Edit: 05.11.19
        Reason: Change start function to awake to get reference on object earlier. Changed controls to use input manager, added reference to panel to avoid constantly calling GameObject.Find
    */
    //File to get Dialgoue
    public DialogueFile VendorSpeech;

    //Inventories
    public Inventory Pinv, VInv;
    public InvSlot pInvSlot,VInvSlot;

    //Do it once
    bool happened = false;
    bool Sell = false;
    bool active = false;

    //X position of the inventory 
    const int PANEL_POSITION_VENDOR_ON = 250;

    // Jonathan Edit
    private DialogueScript DS;
    public bool IsUsingVendor;

    //Andreas edit
    SoundPlayer audioManager;

    //Lewis edit
     InputManager IM;
    public List<GameObject> Panel = new List<GameObject>();
    bool TempWaitBool = false; //temp add by LC

    public List<int> itemsBeingSold;

    void Awake()//changed to awake so can get reference before Inventory panel is deactived
    {
        //Andreas edit
        audioManager = FindObjectOfType<SoundPlayer>();

        //Lewis Edit
        IM = FindObjectOfType<InputManager>();
        Panel.Add(GameObject.Find("SellOrBuyPanel"));
        foreach (Transform t in Panel[0].transform) Panel.Add(t.gameObject);
        // Jonathan Edit
        Pinv = Panel[3].GetComponent<Inventory>();
        VInv = Panel[4].GetComponent<Inventory>();

        DS = FindObjectOfType<DialogueScript>();

    }
    bool delayed = false;
    IEnumerator delay()
    {
        delayed = true;
        yield return new WaitForSeconds(0.2f);
        delayed = false;
    }

    private void Update() //Update is run every frame. A lot of what is currently in update only needs to run once and so should go in a seperate function rather than running all the time. Comment added by LC
    {
        if (active)
        {
            FindObjectOfType<PlayerMovement>().enabled = !(Pinv.isOpen || VInv.isOpen);

            pInvSlot = Pinv.Slots[Pinv.selected];
            VInvSlot = VInv.Slots[VInv.selected];

            if (IsUsingVendor)
            {
                //Switch between buy and sell
                if (IM.Button_B() && !delayed)
                {
                    Sell = !Sell;
                    StartCoroutine(delay());
                }
                //Close, actual closing of the interface handled inside inventory
                if (IM.Button_Menu())
                {
                    IsUsingVendor = false;
                    active = false;
                }
                if (IM.Button_A() && Pinv.isOpen) //edited by LC for input controls
                {
                    if (pInvSlot.hasItem && Sell)
                    {
                        if (pInvSlot.quantity > 1)
                        {
                            //Remove item from player inventory in relation with the quantity we have 
                            Pinv.addItem(Pinv.selected, pInvSlot.quantity, !Sell, !Sell);

                            //Add a item into the vendor inventory 
                            VInv.addItem(VInv.selected, VInvSlot.quantity, !Sell, Sell);

                        }
                        else if (pInvSlot.quantity == 1)
                        {
                            Pinv.addItem(Pinv.selected, pInvSlot.quantity, Sell, !Sell);
                            VInv.addItem(VInv.selected, VInvSlot.quantity, !Sell, Sell);
                        }
                        Pinv.addCoins(pInvSlot.Value);

                        audioManager.Play("Sell");
                    }
                    else if (VInvSlot.hasItem & !Sell)
                    {
                        if (Pinv.getCoins() >= VInvSlot.Value)
                        {
                            if (VInvSlot.quantity > 1)
                            {
                                Pinv.addItem(Pinv.selected, pInvSlot.quantity, Sell, !Sell);
                                VInv.addItem(VInv.selected, VInvSlot.quantity, Sell, Sell);
                            }
                            else if (VInvSlot.quantity == 1)
                            {
                                Pinv.addItem(Pinv.selected, pInvSlot.quantity, Sell, !Sell);
                                VInv.addItem(VInv.selected, VInvSlot.quantity, !Sell, Sell);
                            }

                            Pinv.addCoins(-VInvSlot.Value);
                            audioManager.Play("Buy");
                        }
                    }
                }
            }

            FindObjectOfType<PlayerMovement>().enabled = !active;

            Pinv.active = Sell;
            VInv.active = !Sell;

            // Check to see if the file been read is actually the one on the vendor.....
            // Shouldn't cause problems anymore......
            if (DS.File == VendorSpeech)
            {
                if (DS.FileHasEnded && !happened)
                {
                    IsUsingVendor = true;
                    SellOrBuy();
                    happened = !happened;
                }
            }
        }

    }

    bool exited = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Resets itself everytime you get into the interactable zone
        if (collision.gameObject.name == "Hero")
        {
            if (exited)
            {
                exited = false;
                happened = false;
                active = true;
                //Play speech - edited by jonathan to use findoftype
                int i;
                //RESET INVENTORY
                for (i = 0; i < VInv.items.Length; i++)
                {
                    VInv.items[i] = false;
                    VInv.Slots[i].hasItem = false;
                    VInv.Slots[i].quantity = 0;
                    VInv.Slots[i].updateIcon();
                    //VInv.Slots[i].ID = -1;
                }
                //FILL INVENTORY
                for (i = 0; i < itemsBeingSold.Count; i++) VInv.addItem(itemsBeingSold[i], 99, false);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            exited = true;
        }
    }


    public void SellOrBuy()
    {

        Pinv.open();
        Pinv.VendorMode = true;
        VInv.open();
        VInv.VendorMode = true;

        IsUsingVendor = true;

    }


}
