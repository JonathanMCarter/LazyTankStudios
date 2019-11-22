using UnityEngine;
public class SpawnSetterTemp: A {
 void Start() {
  Transform go = F("Hero").transform;
  go.position = new Vector3(transform.position.x, transform.position.y, go.position.z);
 }
}