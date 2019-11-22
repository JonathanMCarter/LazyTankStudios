﻿using UnityEngine;
public class LightDoor: A {
 public int TotalTiles;
 public BoxCollider2D Door;
 public Sprite Open, Closed;
 public SpriteRenderer me;
 public int LitTiles;
 void Start() {
  me = G<SpriteRenderer>();
 }
 void Update() {
  if (LitTiles >= TotalTiles) {
   Door.enabled = false;
   me.sprite = Open;
  } else {
   Door.enabled = true;
   me.sprite = Closed;
  }
 }
}