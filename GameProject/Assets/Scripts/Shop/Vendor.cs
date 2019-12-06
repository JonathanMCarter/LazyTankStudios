﻿using UnityEngine;
using UnityEditor;
public class Vendor: A {

    public GameObject p;
    public int price;
    bool happened = true;
    CanvasGroup cg;
    InputManager IM;
    Inventory inv;
    private void Start()
    {
        cg = G<CanvasGroup>(F("VendorInventory"));
        IM = F<InputManager>();
        inv = G<Inventory>(F("PlayerInventory"));
    }
    private void Update()
    {
        if (F<DialogueScript>().FileHasEnded && !happened)
        {
            cg.alpha = 1;
            happened = !happened;
        }
        else if (happened && IM.Button_Menu())//Input.GetKeyDown(KeyCode.Escape)) changed by LC - always use IM buttons to allow for cross platform input
            cg.alpha = 0;
        G<PlayerMovement>(F("Hero")).enabled = cg.alpha == 1 ? false : true;
        if (IM.Button_A() && cg.alpha == 1 && inv.getCoins() >= price)
        {
            // p.GetComponent<InventoryItem>().pickup();
            if (p.GetComponent<InventoryItem>().ID == 4) inv.AddPotion();
            else inv.items.Add(p.GetComponent<InventoryItem>().ID);
            F<SoundPlayer>().Play("Pick_Up_Item_1");
            inv.addCoins(-price);
        }
        //if (Input.GetKeyDown(KeyCode.Return))
        //    inv.addCoins(50);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        happened = false;
    }
}
