using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Fader: A {
 Image Img;
 Text Txt;
 void Awake() {
  gameObject.SetActive(false);
 }
 void Start() {
  Img = GetComponentInChildren < Image > ();
  Txt = GetComponentInChildren < Text > ();
  StartCoroutine(Fade());
 }
 IEnumerator Fade() {
  for (int i = 0; i < 10; i++) {
   UpdateColor();
   yield
   return new WaitForSeconds(0.1f);
  }
  Destroy(gameObject, 4.5f);
 }
 void UpdateColor() {
  Color c = Img.color;
  c.a += 0.1f;
  Img.color = c;
  c = Txt.color;
  c.a += 0.1f;
  Txt.color = c;
 }
}