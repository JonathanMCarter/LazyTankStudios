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

        Also Edited by: Lewis Cleminson
        Last Edit: 05.11.19
        Reason: Change start function to awake to get reference on object earlier. Changed controls to use input manager, added reference to panel to avoid constantly calling GameObject.Find
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

    //Andreas edit
    private AudioManager audioManager;

    //Lewis edit
    private InputManager IM;
    private GameObject Panel;
    private bool TempWaitBool = false; //temp add by LC

    public List<int> itemsBeingSold;

    private void Awake()//changed to awake so can get reference before Inventory panel is deactived
    {
        //Andreas edit
        audioManager = FindObjectOfType<AudioManager>();

        //Lewis Edit
        IM = FindObjectOfType<InputManager>();

        Panel = GameObject.Find("SellOrBuyPanel");
        // Jonathan Edit
        inventory = Panel.transform.GetChild(2).GetComponent<Inventory>();
        VendorInventory = Panel.transform.GetChild(3).GetComponent<Inventory>();

        if (Panel.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            Panel.transform.GetChild(0).gameObject.SetActive(false);
            Panel.transform.GetChild(1).gameObject.SetActive(false);
        }

        DS = FindObjectOfType<DialogueScript>();

        //Retrieve inventories
        //inventory = GameObject.Find("PlayerInventory").GetComponent<Inventory>();
        //VendorInventory = GameObject.Find("VendorInventory").GetComponent<Inventory>();

    }


    private void Update() //Update is run every frame. A lot of what is currently in update only needs to run once and so should go in a seperate function rather than running all the time. Comment added by LC
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
        StartCoroutine(delay());//Check note on delat

        if (isSellOrBuyPanelOpened())
        {
            Panel.transform.GetChild(selected).GetComponent<Image>().color = new Color(0.745283f, 0.745283f, 0.745283f);

            if (IM.Y_Axis() == 1 && selected < 2 && selected > 0) //edited by LC for input controls
            {
                StartCoroutine(delay()); //check note on delay
                selected--;

            }
            else if (IM.Y_Axis() == -1 && selected >= 0 && selected < 1) //edited by LC for input controls
            {
                StartCoroutine(delay()); //check note on delay
                selected++;

            }
            Panel.transform.GetChild(selected).GetComponent<Image>().color = new Color(1, 1, 1);

        }

        if (IsUsingVendor)
        {
            if ( IM.Button_A() && inventory.isOpen) //edited by LC for input controls
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
                    inventory.addCoins(VendorInventorySlot.BuyingValue);

                    audioManager.Play("Sell");
                }
                else if (VendorInventorySlot.hasItem & !Sell)
                {
                    if (inventory.getCoins() >= VendorInventorySlot.SellingValue)
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

                        inventory.addCoins(-VendorInventorySlot.SellingValue);
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
                if (!Panel.transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    Panel.transform.GetChild(0).gameObject.SetActive(true);
                    Panel.transform.GetChild(1).gameObject.SetActive(true);
                }

                VendorInventory.gameObject.SetActive(false);

                ToogleSellorBuyPanel(1);

                if (isSellOrBuyPanelOpened())
                {
                    GameObject.Find("Hero").GetComponent<PlayerMovement>().enabled = !isSellOrBuyPanelOpened();
                }


                // if (Input.GetKeyDown(KeyCode.Return)) //currently gets a key that is coded in, does not work on Mobile controls, or easily changed by updating input manager without having to delve into individual scripts. Updated to go off input manager
                if (IM.Button_A() && TempWaitBool) //Changed by LC
                {
                    switch (selected)
                    {
                        case 0:
                            SellOrBuy(true);
                            TempWaitBool = false;
                            break;
                        case 1:
                            SellOrBuy(false);
                            TempWaitBool = false;
                            break;
                    }

                    //this only happens once
                    happened = !happened;

                }
                else if (IM.Button_A() && !TempWaitBool) TempWaitBool = true;

            }
        }

       // if (Input.GetKeyDown(KeyCode.Space))//changed by LC
       if (IM.Button_Menu())
            {
            if (!VendorInventory.gameObject.activeInHierarchy) VendorInventory.gameObject.SetActive(true); //added by LC
            VendorInventory.StartCoroutine(VendorInventory.ToogleSlots(Sell)); //could not start co-routine as VendorInventory is Inactive
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
            if (inventory.transform.localPosition.x > -PANEL_POSITION_VENDOR_ON)
                inventory.VendorPanelPosition(PANEL_POSITION_VENDOR_ON);

            if (VendorInventory.transform.localPosition.x < PANEL_POSITION_VENDOR_ON)
                VendorInventory.VendorPanelPosition(-PANEL_POSITION_VENDOR_ON);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        { 
        //Reset the main inventory on leaving
        inventory.VendorPanelPosition(-PANEL_POSITION_VENDOR_ON);
        }
    }


    public void SellOrBuy(bool action)
    {
        VendorInventory.gameObject.SetActive(true); //added by LC
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
        Panel.transform.GetChild(0).gameObject.SetActive(false);
        Panel.transform.GetChild(1).gameObject.SetActive(false);
        IsUsingVendor = true;

        //ToogleSellorBuyPanel(0);

    }

    void ToogleSellorBuyPanel(int state)
    {
        Panel.GetComponent<CanvasGroup>().alpha = state;
    }

    bool isSellOrBuyPanelOpened()
    {
        return Panel.GetComponent<CanvasGroup>().alpha == 1 ? true : false;

    }
    IEnumerator delay() 
        //Ienumerators create a seperate threat seperate from the main Update / code being run in Unity. This means that when a coroutine (ienumerator) is called, the function is carried out in parallel to the function it was called from, and the original function carried on as usual. 
        //This is seperate to calling a normal function, which will run that function and then go back to the function it was called from. This means the delay being called here does not pause the function the coroutine is created in.
    {
        yield return new WaitForSeconds(0.1f); //due to code above this waits but does nothing after that. Comments added by LC
    }



}
