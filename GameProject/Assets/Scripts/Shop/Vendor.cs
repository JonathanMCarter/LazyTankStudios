﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Vendor : A
{
    public DialogueFile VendorSpeech;
    Inventory Pinv, VInv;
    InvSlot pInvSlot,VInvSlot;
    bool happened;
    bool Sell;
    bool active;
    DialogueScript DS;
    bool IsUsingVendor;
    SoundPlayer audioManager;
    InputManager IM;
    public List<int> itemsSold;
    void Awake()
    {
        audioManager = FindObjectOfType<SoundPlayer>();
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

    void Update()
    {
        if (active)
        {
            pInvSlot = Pinv.Slots[Pinv.selected];
            VInvSlot = VInv.Slots[VInv.selected];
            if (IsUsingVendor)
            {
                if (IM.Button_B() && !delayed)
                {
                    Sell = !Sell;
                    StartCoroutine(delay());
                }
                if (IM.Button_Menu())
                {
                    IsUsingVendor = false;
                    active = false;
                    Sell = true;
                }
                if (IM.Button_A()) 
                {
                    if (pInvSlot.hasItem && Sell)
                    {
                        Pinv.addItem(Pinv.selected, pInvSlot.quantity, pInvSlot.quantity <= 1, !Sell);
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
        if (collision.gameObject.name == "Hero")
        {
            if (exited)
            {
                exited = false;
                happened = false;
                active = true;
                for (int i = 0; i < VInv.items.Length; i++)
                {
                    VInv.items[i] = false;
                    VInv.Slots[i].hasItem = false;
                    VInv.Slots[i].quantity = 0;
                    VInv.Slots[i].updateIcon();
                }
                itemsSold.ForEach(i => VInv.addItem(i, 98, false, true));
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero") exited = true;
    }
}
