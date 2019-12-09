using UnityEngine;
public class TailorCostumeWindow : A{
public void EnableCostumeWindow(){
F<PlayerMovement>().stopInput=true;
F<PlayerMovement>().G<Rigidbody2D>().velocity=Vector2.zero;
C(GameObject.FindGameObjectWithTag("CostumeSwap").transform,0).gameObject.SetActive(true);}}