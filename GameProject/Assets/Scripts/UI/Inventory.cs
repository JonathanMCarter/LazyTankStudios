﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory: A {
 public bool[] items;
 public int equippedA = -1;
 public int equippedB = -1;
 public InvSlot[] Slots;
 public bool isOpen;
 public bool active = true;
 InputManager IM;
 static int Coins = 0;
 const int COLUMN = 4;
 const int ROWS = 3;
 public int selected = 0;
 int column = 4;
 public bool VendorMode;
 SoundPlayer audioManager;
 Text cointext;
 bool delayed;
 public List < string > itemNames = new List < string > ();
 public List < string > itemDescriptions = new List < string > ();
 public CanvasGroup info;
 public Text n, d;
 Inventory activeI;
 public Inventory vendor;
 void Awake() {
  Slots = GetComponentsInChildren < InvSlot > ();
  cointext = GameObject.Find("Coins").GetComponentInChildren < Text > ();
 }
 void Start() {
  addCoins(0);
  items = new bool[Slots.Length];
  IM = FindObjectOfType < InputManager > ();
  audioManager = FindObjectOfType < SoundPlayer > ();
  activeI = this;
 }
 void OnLevelWasLoaded(int level) {
  addCoins(0);
 }
 void Update() {
  if (CompareTag("Inv")) {
   if (activeI.VendorMode) {
    activeI = active ? this : vendor;
   }
   int ID = activeI.selected;
   if (activeI.hasItem(ID)) {
    n.text = itemNames[ID] != null ? itemNames[ID] : "";
    if (activeI.VendorMode) n.text += " " + activeI.Slots[ID].Value + "G";
    d.text = itemDescriptions[ID] != null ? itemDescriptions[ID] : "";
   } else {
    n.text = "";
    d.text = "";
   }
   info.alpha = activeI.isOpen ? n.text.Length == 0 ? 0 : 1 : 0;
  }
  GetComponent < CanvasGroup > ().alpha = isOpen ? 1 : 0;
  if (isOpen && !delayed) {
   if (IM.Button_Menu()) {
    open();
    del();
   }
   if (!VendorMode) {
    if (IM.Button_A()) {
     equipItem(selected, !isEquipped(selected, 1), 1);
     del();
    }
    if (IM.Button_B()) {
     equipItem(selected, !isEquipped(selected, 2), 2);
     del();
    }
   }
   if (active) {
    if (IM.X_Axis() == 1) {
     if (selected < column - 1) selected++;
     del();
    } else if (IM.X_Axis() == -1) {
     if (selected > column - 1 - ROWS) selected--;
     del();
    }
    if (IM.Y_Axis() == 1) {
     if (selected - COLUMN >= 0) {
      selected -= COLUMN;
      column -= COLUMN;
     }
     del();
    } else if (IM.Y_Axis() == -1) {
     if (selected + COLUMN < items.Length) {
      selected += COLUMN;
      column += COLUMN;
     }
     del();
    }
   }
   for (int i = 0; i < Slots.Length; i++) {
    Slots[i].selected = i == selected;
   }
  }
 }
 public void addItem(int id, int quantity, bool remove, bool Increase) {
  quantity = Increase ? quantity += 1 : quantity -= 1;
  items[id] = !remove;
  Slots[id].hasItem = !remove;
  Slots[id].quantity = remove ? 0 : quantity;
  Slots[id].updateIcon();
  audioManager.Play("Pick_Up_Item_1");
 }
 public bool hasItem(int id) {
  return items[id];
 }
 void equipItem(int id, bool equip, int button) {
  if (items[id]) {
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
 bool isEquipped(int id, int button) {
  return Slots[id].equipped == button;
 }
 public void open() {
  isOpen = !isOpen;
  string effectToPlay = isOpen ? "Open_Inventory_1" : "Close_Inventory_1";
  audioManager.Play(effectToPlay);
  if (!isOpen && !VendorMode) GameObject.FindGameObjectWithTag("Map").SetActive(false);
  VendorMode = false;
  FindObjectOfType < PlayerMovement > ().enabled = !isOpen;
  selected = 0;
  column = 4;
 }
 public int getCoins() {
  return Coins;
 }
 public void addCoins(int coinsToBeAdded) {
  Coins += coinsToBeAdded;
  cointext.text = Coins + "";
 }
 void del() {
  StartCoroutine(delay());
 }
 IEnumerator delay() {
  delayed = true;
  yield
  return new WaitForSeconds(0.2f);
  delayed = false;
 }
}