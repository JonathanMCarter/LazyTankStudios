﻿using UnityEngine;
using UnityEngine.UI;
public class InvSlot: A {
 public bool hasItem = false;
 public int quantity = 0;
 public int equipped = 0;
 public bool selected = false;
 public int Value;
 Image img;
 public bool recievesXP = false;
 public GameObject i;
 public Text q, e;
 void Start() {
  img = GetComponent < Image > ();
  updateIcon();
  selected = false;
 }
 public void updateIcon() {
  i.SetActive(hasItem);
  q.text = quantity > 0 ? quantity + "" : "";
  e.text = equipped > 0 ? equipped == 1 ? "A" : "B" : "";
 }
 public void equip(int equip) {
  equipped = equip;
  updateIcon();
 }
 void Update() {
  img.color = selected ? new Color(0, 0, 1) : new Color(0.35 f, 0.35 f, 0.35 f);
 }
}