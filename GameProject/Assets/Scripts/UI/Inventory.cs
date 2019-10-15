﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Toby Wishart
 * Last edit: 11/10/19
 * Also edited: Gabriel Potamianos
 * Last edit: 14/10/19
 * 
 * 
 * 
 * Class to handle inventory
 * Uses very simple method of storing the items as they are in fixed slots
 * 
 * Reason - Scales the inventory window into a smaller one while vendorMode active
 *        - Bug fixed: Menu activates player movement when it is closed before inventory
 *  
 */
public class Inventory : MonoBehaviour
{

    bool[] items;
    public int equipped = -1;

    int selected = -1;
    public bool isOpen;
    public const int rows=3;


    [HideInInspector]
    public bool vendorMode;


    //Add item to inventory with quantity, bool to remove as well can also use this to increase quantity
    public void addItem(int id, int quantity, bool remove)
    {
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
    public void equipItem(int id, bool equip)
    {
        if (items[id])
        {
            InvSlot slot = transform.GetChild(id).GetComponent<InvSlot>();
            slot.equip(equip);
            equipped = equip ? id : -1;
            for (int i = 0; i < items.Length; i++) transform.GetChild(i).GetComponent<InvSlot>().equip(i == id && equip);
        }
    }

    public bool isEquipped(int id)
    {
        return transform.GetChild(id).GetComponent<InvSlot>().equipped;
    }

    //Open/close inventory
    public void open()
    {
        isOpen = !isOpen;
        selected = -1;
    }

    void Start()
    {
        items = new bool[transform.childCount];
        vendorMode = gameObject.name.Equals("VendorInventory");
        print(vendorMode);
    }

    private bool delayed = false;
    void Update()
    {

        GetComponent<CanvasGroup>().alpha = isOpen ? 1 : 0;
        if (isOpen && !delayed)
        {

            if (Input.GetButtonDown("Cancel"))
            {
                open();
                StartCoroutine(delay());
            }
            if (Input.GetButtonDown("Submit") && !vendorMode)
            {
                equipItem(selected, !isEquipped(selected));
                StartCoroutine(delay());
            }
            if (Input.GetAxisRaw("Horizontal") == 1 && !vendorMode)
            {
                if (selected >= rows) selected -= rows;
                else selected++;
                StartCoroutine(delay());
            }
            else if (Input.GetAxisRaw("Horizontal") == -1 && !vendorMode)
            {
                if (selected > 1+(selected/rows)) selected--;
               // else selected--;
                StartCoroutine(delay());
            }


            if (Input.GetAxisRaw("Vertical") == 1 && !vendorMode)
            {
                if (selected-items.Length/rows< 0) selected += items.Length - items.Length/rows;
                else selected -= items.Length/rows;
                StartCoroutine(delay());
            }
            else if (Input.GetAxisRaw("Vertical") == -1 && !vendorMode)
            {
                if (selected + items.Length/rows >= items.Length) selected -= items.Length - items.Length/rows;
                else selected += items.Length/rows;
                StartCoroutine(delay());
            }
            for (int i = 0; i < items.Length; i++)
                transform.GetChild(i).GetComponent<InvSlot>().selected = i == selected;

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

    //Changes position of the inventory panel while in vendor mode
    public void ToogleVendorModeON(int value)
    {

        gameObject.transform.position = new Vector3(transform.position.x - value, transform.position.y, transform.position.z);
    }



}
