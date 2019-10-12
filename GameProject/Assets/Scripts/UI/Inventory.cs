using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Toby Wishart
 * Last edit: 11/10/19
 * 
 * Class to handle inventory
 * Uses very simple method of storing the items as they are in fixed slots
 */
public class Inventory : MonoBehaviour
{
    bool[] items;
    public int equipped = -1;

    int selected = 0;
    public bool isOpen;
    
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
    }

    void Start()
    {
        items = new bool[transform.childCount];
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
            if (Input.GetButtonDown("Submit"))
            {
                equipItem(selected, !isEquipped(selected));
                StartCoroutine(delay());
            }
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                selected++;
                if (selected == items.Length) selected = 0;
                StartCoroutine(delay());
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                selected--;
                if (selected == -1) selected = items.Length-1;
                StartCoroutine(delay());
            }
            for (int i = 0; i < items.Length; i++)
                transform.GetChild(i).GetComponent<InvSlot>().selected = i == selected; 
        }
    }

    IEnumerator delay()
    {
        delayed = true;
        yield return new WaitForSeconds(0.1f);
        delayed = false;
    }
}
