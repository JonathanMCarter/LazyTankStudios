//using UnityEngine;
//public class CollectableItem: A {
// InputManager IM;
// Quest aQ;
// void Start() {
//  IM = F<InputManager>();
// }
// void OnTriggerStay2D(Collider2D collision) {
//  if ((collision.gameObject.name == "Hero") && IM.Button_A()) {
//   aQ = getActiveQuests();
//   for (int i = 0; i < aQ.ItemsToBeCollected.Length; i++) {
//    if (aQ.ItemsToBeCollected[i] == G<SpriteRenderer>().sprite) {
//     aQ.ItemsQuantities[i]--;
//     D(gameObject);
//    }
//   }
//  }
// }
// Quest getActiveQuests() {
//  foreach(Quest quest in Fs<Quest>()) if (quest.CollectRequest && quest.isActiveAndEnabled) return quest;
//  return null;
// }
//}