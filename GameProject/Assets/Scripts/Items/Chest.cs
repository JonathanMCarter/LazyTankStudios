using System.Collections.Generic;
using UnityEngine;
public class Chest: A {
public List < GameObject > contents;
public void open() {
G<Animator>().SetTrigger("open");
foreach(GameObject g in contents) {
GameObject newG = Instantiate(g);
newG.transform.position = new Vector3(transform.position.x + Random.Range(-1F, 1F), transform.position.y + Random.Range(-1F, 1F), transform.position.z - 1);}
G<Interact> ().Enabled = false;}}