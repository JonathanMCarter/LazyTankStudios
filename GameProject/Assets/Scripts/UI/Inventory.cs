using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Created by Toby Wishart
 * Last edit: 10/11/19
 * Reason: Added item levelling
 * Also edited: Gabriel Potamianos
 * Last edit: 14/10/19
 * 
 * Also edited: Andreas Kraemer
 * Last edit: 21/10/19
 * 
 * 
 * Class to handle inventory
 * Uses very simple method of storing the items as they are in fixed slots
 * 
 * Reason - Scales the inventory window into a smaller one while vendorInventory active
 *        - Bug fixed: Menu activates player movement when it is closed before inventory
 *  
 */
public class Inventory : A
{

    public bool[] items;
    //ID for the item equipped with the A button
    public int equippedA = -1;
    //ID for the item equipped with the B button
    public int equippedB = -1;

    //array to store the xp for each item
    //public int[] itemXP;

   // Dictionary<int, InvSlot> slots = new Dictionary<int, InvSlot>();
    public InvSlot[] Slots;
    public bool isOpen;

    InputManager IM;

    static int Coins = 0;



    [HideInInspector]
    public const int COLUMN = 4;
    public const int ROWS = 3;
    public int selected = 0;
    int column = 4;
    public bool VendorMode = false;
    SoundPlayer audioManager;
    Text cointext;
    bool delayed = false;


    void Awake()
    {
        Slots = GetComponentsInChildren<InvSlot>();
        cointext = GameObject.Find("Coins").GetComponentInChildren<Text>();
    }

    void Start()
    {
        
        addCoins(0);
       // int count = 0;
        //foreach (Transform t in transform)
        //{
        //    InvSlot slot = t.gameObject.GetComponent<InvSlot>();
        //    if (slot.ID >= 0) slots.Add(slot.ID, slot);
        //    if (t.gameObject.activeSelf) count++;
        //}
        items = new bool[Slots.Length];
        //itemXP = new int[items.Length];
        IM = FindObjectOfType<InputManager>();
        audioManager=FindObjectOfType<SoundPlayer>();
    }

    void OnLevelWasLoaded(int level)
    {
        addCoins(0);
    }


    void Update()
    {
        GetComponent<CanvasGroup>().alpha = isOpen ? 1 : 0;

        if (isOpen && !delayed)
        {

            if (IM.Button_Menu())
            {
                open();
                StartCoroutine(delay());
            }
            if (IM.Button_A() && !VendorMode)
            {
                equipItem(getSelectedID(), !isEquipped(getSelectedID(), 1), 1);
                StartCoroutine(delay());
            }

            if (IM.Button_B() && !VendorMode)
            {
                equipItem(getSelectedID(), !isEquipped(getSelectedID(), 2), 2);
                StartCoroutine(delay());
            }



            //Lock selector in horizontal depending on which row it is (RIGHT END)
            if (IM.X_Axis() == 1 )
            {
                //if (VendorMode)
                //{
                    if (selected < column - 1) selected++;
                //} //else
                //{
                    //selected++;
                    //if (selected == items.Length) selected--;
                //}
                StartCoroutine(delay());
            }

            //Lock selector on horizontal depending on which row it is (LEFT END)
            else if (IM.X_Axis() == -1 )
            {
                //if (VendorMode)
                //{
                    if (selected > column - 1 - ROWS) selected--;
                //}
                //else
                //{
                    //selected--;
                    //if (selected == -1) selected++;
                //}
                StartCoroutine(delay());
            }

            //Lock selector on vertical depending whether it exceeds the limits (LEFT END)
            if (IM.Y_Axis() == 1 )
            {
                if (selected - COLUMN >= 0)
                {
                    selected -= COLUMN;
                    column -= COLUMN;
                }
                StartCoroutine(delay());
            }

            //Lock selector on vertical depending whether it exceeds the limits (RIGHT END )
            else if (IM.Y_Axis() == -1)
            {
                if (selected + COLUMN < items.Length)
                {
                    selected += COLUMN;
                    column+=COLUMN;
                }
                StartCoroutine(delay());
            }



            //Emphasize the slot 
            for (int i = 0; i < items.Length; i++)
            {
                //Andreas edit
                //transform.GetChild(i).GetComponent<InvSlot>().selected = i == selected;
                InvSlot slot = transform.GetChild(i).GetComponent<InvSlot>();
                slot.selected = (i == selected);

            }


        }
    }



    //Add item to inventory with quantity, bool to remove as well can also use this to increase quantity
    public void addItem(int id, int quantity, bool remove)
    {
        items[id] = !remove;
        Slots[id].hasItem = !remove;
        Slots[id].quantity = remove ? 0 : quantity;
        Slots[id].updateIcon();
        //print(slot.quantity);
        //Andreas edit--
        audioManager.Play("Pick_Up_Item_1");
        //Andreas edit end--
    }
    public void addItem(int id, int quantity, bool remove, bool Increase)
    {
        quantity = Increase ? quantity += 1 : quantity -= 1;
        items[id] = !remove;
        Slots[id].hasItem = !remove;
        Slots[id].quantity = remove ? 0 : quantity;
        Slots[id].updateIcon();
        //Andreas edit--
        audioManager.Play("Pick_Up_Item_1");
        //Andreas edit end--
    }

    public bool hasItem(int id)
    {
        return items[id];
    }

    public int getSelectedID()
    {
        return Slots[selected].ID;
    }


    public void equipItem(int id, bool equip, int button)
    {
        if (items[id])
        {
            Slots[id].equip(equip ? button : 0);
            switch (button) {
                case 1:
                    equippedA = equip ? id : -1;
                    equippedB = equippedA == equippedB ? -1 : equippedB;
                    break;
                case 2:
                    equippedB = equip ? id : -1;
                    equippedA = equippedA == equippedB ? -1 : equippedA;
                    break;
            }
            for (int i = 0; i < items.Length; i++) Slots[i].equip(i == equippedA ? 1 : i == equippedB ? 2 : 0);
        }
    }

    public bool isEquipped(int id, int button)
    {
        return Slots[id].equipped == button;
    }

    ///Open/close inventory
    public void open()
    {
        isOpen = !isOpen;
        //Andreas edit--
        string effectToPlay = isOpen ? "Open_Inventory_1" : "Close_Inventory_1";
        audioManager.Play(effectToPlay);
        //if(!isOpen)FindObjectOfType<GameManager>().isPaused=false;
        if(!isOpen && !VendorMode)GameObject.FindGameObjectWithTag("Map").SetActive(false);
        VendorMode = false;
        // Locks player until the inventory is closed 	
        FindObjectOfType<PlayerMovement>().enabled = !isOpen;
        //Andreas edit end--
        selected = 0;
    }

    //Returns level based on the XP
    //public int getLevel(int ID)
    //{
    //    if (!canItemRecieveXP(ID)) return 1;
    //    return itemXP[ID] < 5 ? 1 : (itemXP[ID] < 20 ? 2 : 3);
    //} 

    //Add(or subtract) XP to given item, limits the value between 0 and 20
    //public void addXP(int ID, int amount)
    //{
    //    //Exit if the item can't level up
    //    if (!canItemRecieveXP(ID)) return;
    //    itemXP[ID] += amount;
    //    //Cap xp
    //    itemXP[ID] = itemXP[ID] < 0 ? 0 : itemXP[ID] > 20 ? 20 : itemXP[ID];
    //}

    ////Returns whether the item can level up 
    //public bool canItemRecieveXP(int ID)
    //{
    //    if (ID < 0 || ID >= items.Length) return false;
    //    return slots[ID].recievesXP;
    //}




    //Toggles the slots and changes the colour while doing that
    public IEnumerator ToggleSlots(bool state)
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < items.Length; i++)
        {
            if (!state)
                //slots[i].UnselectedColourApplied();
            Slots[i].enabled = state;

        }
    }

    //Changes position of the inventory panel while in vendor mode
    //public void VendorPanelPosition(int value)
    //{
    //    //gameObject.transform.position = new Vector3(transform.position.x - value, transform.position.y, transform.position.z);
    //}


    public int getCoins()
    {
        return Coins;
    }

    public void addCoins(int coinsToBeAdded)
    {
        Coins += coinsToBeAdded;
        cointext.text = Coins+"";

    }


    IEnumerator delay()
    {
        delayed = true;
        yield return new WaitForSeconds(0.2f);
        delayed = false;
    }



}
