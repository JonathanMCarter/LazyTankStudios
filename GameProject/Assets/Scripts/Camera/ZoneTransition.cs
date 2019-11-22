using UnityEngine;
public class ZoneTransition: A {
 public string destination;
 void OnTriggerEnter2D(Collider2D collision) {
  if (collision.gameObject.CompareTag("Player")) {
   F<PlayerMovement>().load(destination.Split(':'));
  }
 }
}