using UnityEngine;
public class Portal: A {
public GameObject portal;
public float XOffset, YOffset;
void OnTriggerStay2D(Collider2D collision) {
if (collision.gameObject.name == "Hero") collision.transform.position = new Vector2(portal.transform.position.x + XOffset, portal.transform.position.y + YOffset);}}