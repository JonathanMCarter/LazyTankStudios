//using System.Collections;
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
    bool Sell = true;
    int selected = 0;

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

        if (Panel[1].activeInHierarchy)
        {
            Panel[1].SetActive(false);
            Panel[2].SetActive(false);
        }

        DS = FindObjectOfType<DialogueScript>();

        //Retrieve inventories
        //inventory = GameObject.Find("PlayerInventory").GetComponent<Inventory>();
        //VendorInventory = GameObject.Find("VendorInventory").GetComponent<Inventory>();

    }


    private void Update() //Update is run every frame. A lot of what is currently in update only needs to run once and so should go in a seperate function rather than running all the time. Comment added by LC
    {
        if (!Pinv.gameObject.activeInHierarchy)
        {
            Pinv.gameObject.SetActive(true);
        }

        if (!VInv.gameObject.activeInHierarchy)
        {
            VInv.gameObject.SetActive(true);
        }

        pInvSlot = Pinv.transform.GetChild(Pinv.selected).GetComponent<InvSlot>();
        VInvSlot = VInv.transform.GetChild(VInv.selected).GetComponent<InvSlot>();
       // StartCoroutine(delay());//Check note on delat

        if (isSellOrBuyPanelOpened())
        {
            Panel[selected+1].GetComponent<Image>().color = new Color(0.745283f, 0.745283f, 0.745283f);

            if (IM.Y_Axis() == 1 && selected < 2 && selected > 0) //edited by LC for input controls
            {
               // StartCoroutine(delay()); //check note on delay
                selected--;

            }
            else if (IM.Y_Axis() == -1 && selected >= 0 && selected < 1) //edited by LC for input controls
            {
                //StartCoroutine(delay()); //check note on delay
                selected++;

            }
            Panel[selected + 1].GetComponent<Image>().color = new Color(1, 1, 1);

        }

        if (IsUsingVendor)
        {
            if ( IM.Button_A() && Pinv.isOpen) //edited by LC for input controls
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
                    Pinv.addCoins(VInvSlot.BuyingValue);

                    audioManager.Play("Sell");
                }
                else if (VInvSlot.hasItem & !Sell)
                {
                    if (Pinv.getCoins() >= VInvSlot.SellingValue)
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

                        Pinv.addCoins(-VInvSlot.SellingValue);
                        audioManager.Play("Buy");
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
                if (!Panel[0].activeInHierarchy)
                {
                    Panel[0].SetActive(true);
                    Panel[1].SetActive(true);
                }

                Panel[4].SetActive(false); //vendor inventory

                ToogleSellorBuyPanel(1);

                if (isSellOrBuyPanelOpened())
                {
                    FindObjectOfType<PlayerMovement>().enabled = !isSellOrBuyPanelOpened();
                }


                // if (Input.GetKeyDown(KeyCode.Return)) //currently gets a key that is coded in, does not work on Mobile controls, or easily changed by updating input manager without having to delve into individual scripts. Updated to go off input manager
                if (IM.Button_A() && TempWaitBool) //Changed by LC
                {
                    //switch (selected)
                    //{
                    //    case 0:
                    //        SellOrBuy(true);
                    //        break;
                    //    case 1:
                    //        SellOrBuy(false);
                            
                    //        break;
                    //}
                    SellOrBuy(selected == 0);
                    TempWaitBool = false;
                    //this only happens once
                    happened = !happened;

                }
                else if (IM.Button_A() && !TempWaitBool) TempWaitBool = true;

            }
        }

       // if (Input.GetKeyDown(KeyCode.Space))//changed by LC
       if (IM.Button_Menu() && Panel[4].GetComponent<CanvasGroup>().alpha == 1)
            {
            if (!Panel[3].activeInHierarchy) Panel[4].SetActive(true); //added by LC
            VInv.StartCoroutine(VInv.ToggleSlots(Sell)); //could not start co-routine as VendorInventory is Inactive
            Sell = !Sell;
            Pinv.StartCoroutine(Pinv.ToggleSlots(Sell));
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
            int i;
            //RESET INVENTORY
            for (i = 0; i < VInv.items.Length; i++)
            {
                VInv.items[i] = false;
                VInv.Slots[i].hasItem = false;
                VInv.Slots[i].quantity = 0;
                //VInv.Slots[i].ID = -1;
            }
            //FILL INVENTORY
            for (i = 0; i < itemsBeingSold.Count; i++) VInv.addItem(itemsBeingSold[i], 99, false);
            //Position the inventories
            //if (inventory.transform.localPosition.x > -PANEL_POSITION_VENDOR_ON)
            //    inventory.VendorPanelPosition(PANEL_POSITION_VENDOR_ON);

            //if (VendorInventory.transform.localPosition.x < PANEL_POSITION_VENDOR_ON)
            //    VendorInventory.VendorPanelPosition(-PANEL_POSITION_VENDOR_ON);

        }

    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "Hero")
    //    { 
    //    //Reset the main inventory on leaving
    //    //inventory.VendorPanelPosition(-PANEL_POSITION_VENDOR_ON);
    //    }
    //}


    public void SellOrBuy(bool action)
    {
        Panel[4].SetActive(true); //added by LC
        Sell = action;
        if (Sell)
        {
            VInv.StartCoroutine(VInv.ToggleSlots(!Sell));
            Pinv.StartCoroutine(Pinv.ToggleSlots(Sell));
        }
        else
        {
            Pinv.StartCoroutine(Pinv.ToggleSlots(Sell));
            VInv.StartCoroutine(VInv.ToggleSlots(!Sell));
        }


        Pinv.open();
        Pinv.VendorMode = true;
        Panel[4].SetActive(true);
        VInv.open();
        VInv.VendorMode = true;

        // Jonathan Edit - Makes it so it disables the sell / buy buttons
        Panel[1].SetActive(false);
        Panel[2].SetActive(false);
        IsUsingVendor = true;

        //ToogleSellorBuyPanel(0);

    }

    void ToogleSellorBuyPanel(int state)
    {
        Panel[0].GetComponent<CanvasGroup>().alpha = state;
    }

    bool isSellOrBuyPanelOpened()
    {
        return Panel[0].GetComponent<CanvasGroup>().alpha == 1 ? true : false;
    
    }
    //IEnumerator delay() 
    //    //Ienumerators create a seperate threat seperate from the main Update / code being run in Unity. This means that when a coroutine (ienumerator) is called, the function is carried out in parallel to the function it was called from, and the original function carried on as usual. 
    //    //This is seperate to calling a normal function, which will run that function and then go back to the function it was called from. This means the delay being called here does not pause the function the coroutine is created in.
    //{
    //    yield return new WaitForSeconds(0.1f); //due to code above this waits but does nothing after that. Comments added by LC
    //}



}
