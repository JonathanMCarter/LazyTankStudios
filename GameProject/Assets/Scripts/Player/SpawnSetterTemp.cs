﻿using UnityEngine;
public class SpawnSetterTemp: A {
 void Start() {
  Transform go = GameObject.Find("Hero").transform;
  go.position = new Vector3(transform.position.x, transform.position.y, go.position.z);
 }
}