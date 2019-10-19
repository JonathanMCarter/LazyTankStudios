using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Toby Wishart
 * Last edit: 19/10/19
 * Also edited: Gabriel Potamianos
 * Last edit: 14/10/19
 * 
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

    bool[] items;
    //ID for the item equipped with the A button
    public int equippedA = -1;
    //ID for the item equipped with the B button
    public int equippedB = -1;

    public bool isOpen;

    InputManager IM;


    #region Gabriel Variables

    [HideInInspector]
    public const int ROWS = 3;
    public const int COLUMN = 4;
    public int selected = 0;
    int column = 4;
    public bool VendorMode { get; set; }

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
    }
    public void addItem(int id, int quantity, bool remove,int QuantityToBeChangedWith,bool Increase)
    {
        quantity = Increase ? quantity += 1 : quantity -= 1;
        items[id] = !remove;
        InvSlot slot = transform.GetChild(id).GetComponent<InvSlot>();
        slot.hasItem = !remove;
        slot.quantity = remove ? 0 : quantity;
        slot.updateIcon();
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
        selected = 0;
    }

    #endregion

    #region Gabriel Functions

    //Toogles the slots and changes the colour while doing that
    public IEnumerator ToogleSlots(bool state)
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < items.Length; i++)
        {
            if(!state)
                 transform.GetChild(i).GetComponent<InvSlot>().UnselectedColourApplied();
            transform.GetChild(i).GetComponent<InvSlot>().enabled = state;

        }
    }

    //Changes position of the inventory panel while in vendor mode
    public void VendorPanelPosition(int value)
    {
        gameObject.transform.position = new Vector3(transform.position.x - value, transform.position.y, transform.position.z);
    }

    #endregion
    void Start()
    {
        items = new bool[transform.childCount];
        VendorMode = false;

        IM = GameObject.FindObjectOfType<InputManager>();
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
                    if (selected == items.Length) selected = 0;
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
                    if (selected == -1) selected = items.Length - 1;
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
                transform.GetChild(i).GetComponent<InvSlot>().selected = i == selected;

            // Locks player until the inventory is closed 
            GameObject.Find("Hero").GetComponent<PlayerMovement>().enabled = !isOpen;

        }
    }

    IEnumerator delay()
    {
        delayed = true;
        yield return new WaitForSeconds(0.1f);
        delayed = false;
    }





}
