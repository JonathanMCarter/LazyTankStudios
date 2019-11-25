using UnityEngine;
public class TileLight: A {
 public bool Lit;
 public LightDoor Door;
 public Sprite On, Off;
 void Start() {
  //Door = G<LightDoor>(FT("Door"));
 }
 void OnTriggerEnter2D(Collider2D collision) {
  if (collision.CompareTag("Player")) {
   if (!Lit) {
    Door.LitTiles++;
    Lit = true;
    G<SpriteRenderer>().sprite = On;
   } else {
    Door.LitTiles--;
    Lit = false;
    G<SpriteRenderer>().sprite = Off;
   }
  }
 }
}