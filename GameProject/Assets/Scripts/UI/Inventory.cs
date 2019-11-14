﻿using System.Collections;
using System.Collections.Generic;
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
public class Inventory : MonoBehaviour
{

    public bool[] items;
    //ID for the item equipped with the A button
    public int equippedA = -1;
    //ID for the item equipped with the B button
    public int equippedB = -1;

    //array to store the xp for each item
    public int[] itemXP;

    public bool isOpen;

    InputManager IM;

    int Coins=0;

    #region Gabriel Variables

    [HideInInspector]
    public const int ROWS = 3;
    public const int COLUMN = 4;
    public int selected = 0;
    int column = 4;
    public bool VendorMode;

    #endregion

    #region Andreas Variables
    private AudioManager audioManager;
    #endregion

    #region Toby Functions
    //Add item to inventory with quantity, bool to remove as well can also use this to increase quantity
    public void addItem(int id, int quantity, bool remove)
    {
        items[id] = !remove;
        InvSlot slot = transform.GetChild(id).GetComponent<InvSlot>();
        slot.hasItem = !remove;
        slot.quantity = remove ? 0 : quantity;
        slot.updateIcon();
        //print(slot.quantity);
        //Andreas edit--
        //audioManager.Play("Item_PickUp");
        //Andreas edit end--
    }
    public void addItem(int id, int quantity, bool remove, int QuantityToBeChangedWith, bool Increase)
    {
        quantity = Increase ? quantity += 1 : quantity -= 1;
        items[id] = !remove;
        InvSlot slot = transform.GetChild(id).GetComponent<InvSlot>();
        slot.hasItem = !remove;
        slot.quantity = remove ? 0 : quantity;
        slot.updateIcon();
        //Andreas edit--
        //audioManager.Play("Item_PickUp");
        //Andreas edit end--
    }

    public bool hasItem(int id)
    {
        return items[id];
    }

    //Equip or unequip item as long as player has item
    //id = the item ID
    //equip = equipping or unequipping
    //button = the button pressed
    public void equipItem(int id, bool equip, int button)
    {
        if (items[id])
        {
            InvSlot slot = transform.GetChild(id).GetComponent<InvSlot>();
            slot.equip(equip ? button : 0);
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
            for (int i = 0; i < items.Length; i++) transform.GetChild(i).GetComponent<InvSlot>().equip(i == equippedA ? 1 : i == equippedB ? 2 : 0);
        }
    }

    public bool isEquipped(int id, int button)
    {
        return transform.GetChild(id).GetComponent<InvSlot>().equipped == button;
    }

    //Open/close inventory
    public void open()
    {
        isOpen = !isOpen;
        //Andreas edit--
        string effectToPlay = isOpen ? "Inventory_Open" : "Inventory_Close";
        if (audioManager != null) audioManager.Play(effectToPlay);
        if(!isOpen)GameObject.FindGameObjectWithTag("Map").SetActive(false);
        //Andreas edit end--
        selected = 0;
    }

    //Returns level based on the XP
    public int getLevel(int ID)
    {
        if (!canItemRecieveXP(ID)) return 1;
        return itemXP[ID] < 5 ? 1 : (itemXP[ID] < 20 ? 2 : 3);
    } 

    //Add(or subtract) XP to given item, limits the value between 0 and 20
    public void addXP(int ID, int amount)
    {
        //Exit if the item can't level up
        if (!canItemRecieveXP(ID)) return;
        itemXP[ID] += amount;
        //Cap xp
        itemXP[ID] = itemXP[ID] < 0 ? 0 : itemXP[ID] > 20 ? 20 : itemXP[ID];
    }

    //Returns whether the item can level up 
    public bool canItemRecieveXP(int ID)
    {
        if (ID < 0 || ID >= items.Length) return false;
        return transform.GetChild(ID).GetComponent<InvSlot>().recievesXP;
    }

    #endregion

    #region Gabriel Functions

    //Toogles the slots and changes the colour while doing that
    public IEnumerator ToogleSlots(bool state)
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < items.Length; i++)
        {
            if (!state)
                transform.GetChild(i).GetComponent<InvSlot>().UnselectedColourApplied();
            transform.GetChild(i).GetComponent<InvSlot>().enabled = state;

        }
    }

    //Changes position of the inventory panel while in vendor mode
    public void VendorPanelPosition(int value)
    {
        gameObject.transform.position = new Vector3(transform.position.x - value, transform.position.y, transform.position.z);
    }


    public int getCoins()
    {
        return Coins;
    }

    public void addCoins(int coinsToBeAdded)
    {
        Coins += coinsToBeAdded;
        //Toby: "CoinUI" has been renamed to "Coins" and child panel has been removed
        GameObject.Find("Coins").transform.GetChild(1).gameObject.GetComponent<Text>().text = Coins.ToString();

    }


    #endregion

    void Start()
    {
        //GameObject.Find("CoinUI").transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = "0";
        GameObject.Find("Coins").GetComponentInChildren<Text>().text = Coins.ToString(); //changed by LC to match UI hierarchy 
        items = new bool[transform.childCount];
        itemXP = new int[items.Length];
        VendorMode = false;
        IM = GameObject.FindObjectOfType<InputManager>();
        audioManager=GameObject.FindObjectOfType<AudioManager>();
    }

    private bool delayed = false;
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
                equipItem(selected, !isEquipped(selected, 1), 1);
                StartCoroutine(delay());
            }

            if (IM.Button_B() && !VendorMode)
            {
                equipItem(selected, !isEquipped(selected, 2), 2);
                StartCoroutine(delay());
            }

            #region Gabriel Edit Selector Movement

            //Lock selector in horizontal depending on which row it is (RIGHT END)
            if (IM.X_Axis() == 1 )
            {
                if (VendorMode)
                {
                    if (selected < column - 1) selected++;
                } else
                {
                    selected++;
                    if (selected == items.Length) selected--;
                }
                StartCoroutine(delay());
            }

            //Lock selector on horizontal depending on which row it is (LEFT END)
            else if (IM.X_Axis() == -1 )
            {
                if (VendorMode)
                {
                    if (selected > column - 1 - ROWS) selected--;
                } else
                {
                    selected--;
                    if (selected == -1) selected++;
                }
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

            #endregion

            //Emphasize the slot 
            for (int i = 0; i < items.Length; i++)
            {
            //Andreas edit
            //transform.GetChild(i).GetComponent<InvSlot>().selected = i == selected;
                if(i==selected)
                {
                    transform.GetChild(i).GetComponent<InvSlot>().selected=true;
                    transform.GetChild(i).GetComponent<InvSlot>().SelectedColourApplied();
                }
                else transform.GetChild(i).GetComponent<InvSlot>().UnselectedColourApplied();
            }

                

            // Locks player until the inventory is closed 
            GameObject.Find("Hero").GetComponent<PlayerMovement>().enabled = !isOpen;



        }
    }

    IEnumerator delay()
    {
        delayed = true;
        yield return new WaitForSeconds(0.2f);
        delayed = false;
    }
}
