using UnityEngine;
public class ItemCheck: A {
 public int item;
 void Update() {
  if (GameObject.FindGameObjectWithTag("Inv").GetComponent < Inventory > ().hasItem(item)) gameObject.SetActive(false);
 }
}