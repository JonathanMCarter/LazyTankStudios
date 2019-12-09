using UnityEngine;
using UnityEngine.Events;
public class Interact: A {
public bool Enabled = true;
public UnityEvent interact;
void OnTriggerEnter2D(Collider2D collision) {
if (Enabled)
if (collision.gameObject.tag == "Player") interact.Invoke();}}