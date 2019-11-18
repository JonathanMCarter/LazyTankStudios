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
    Inventory Pinv, VInv;
    InvSlot pInvSlot,VInvSlot;

    //Do it once
    bool happened;
    bool Sell;
    bool active;

    // Jonathan Edit
    DialogueScript DS;
    bool IsUsingVendor;

    //Andreas edit
    SoundPlayer audioManager;

    //Lewis edit
    InputManager IM;

    public List<int> itemsSold;

    void Awake()//changed to awake so can get reference before Inventory panel is deactived
    {
        //Andreas edit
        audioManager = FindObjectOfType<SoundPlayer>();

        //Lewis Edit
        IM = FindObjectOfType<InputManager>();

        Inventory[] i = GameObject.Find("SellOrBuyPanel").GetComponentsInChildren<Inventory>();
        Pinv = i[0];
        VInv = i[1];

        DS = FindObjectOfType<DialogueScript>();

    }
    bool delayed;
    IEnumerator delay()
    {
        delayed = true;
        yield return new WaitForSeconds(0.2f);
        delayed = false;
    }

    void Update() //Update is run every frame. A lot of what is currently in update only needs to run once and so should go in a seperate function rather than running all the time. Comment added by LC
    {
        if (active)
        {
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
                    //allow for movement in the player inventory
                    Sell = true;
                }
                if (IM.Button_A()) //edited by LC for input controls
                {
                    if (pInvSlot.hasItem && Sell)
                    {
                        //Remove item from player inventory in relation with the quantity we have 
                        Pinv.addItem(Pinv.selected, pInvSlot.quantity, pInvSlot.quantity <= 1, !Sell);
                        //Add a item into the vendor inventory 
                        VInv.addItem(Pinv.selected, VInv.Slots[Pinv.selected].quantity, !Sell, Sell);
                        Pinv.addCoins(pInvSlot.Value);

                        audioManager.Play("Sell");
                    }
                    else if (VInvSlot.hasItem && !Sell)
                    {
                        if (Pinv.getCoins() >= VInvSlot.Value)
                        {
                            Pinv.addItem(VInv.selected, Pinv.Slots[VInv.selected].quantity, Sell, !Sell);
                            VInv.addItem(VInv.selected, VInvSlot.quantity, VInvSlot.quantity <= 1, Sell);
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
                    Pinv.open();
                    Pinv.VendorMode = true;
                    VInv.open();
                    VInv.VendorMode = true;
                    happened = !happened;
                }
            }
        }

    }

    bool exited = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Resets itself everytime you get into the interactable zone
        if (collision.gameObject.name == "Hero")
        {
            if (exited)
            {
                exited = false;
                happened = false;
                active = true;
                //RESET INVENTORY
                for (int i = 0; i < VInv.items.Length; i++)
                {
                    VInv.items[i] = false;
                    VInv.Slots[i].hasItem = false;
                    VInv.Slots[i].quantity = 0;
                    VInv.Slots[i].updateIcon();
                }
                //FILL INVENTORY
                itemsSold.ForEach(i => VInv.addItem(i, 99, false));
            }
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero") exited = true;
    }
}
