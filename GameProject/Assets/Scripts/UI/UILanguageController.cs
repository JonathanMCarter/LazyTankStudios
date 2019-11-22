using UnityEngine;
using UnityEngine.UI;
public class UILanguageController: A {
 Text[] textObjects;
 void Awake() {
  UpdateLanguage();
 }
 public void UpdateLanguage() {
  try {
   if (F<PlayerMovement>().Menu != null) F<PlayerMovement>().Menu.SetActive(true);
  } catch {}
  textObjects = Fs<Text>();
  foreach(Text text in textObjects) {
   text.enabled = true;
   if ((text.CompareTag("TextEnglish") && !LanguageSelect.isEnglish) || (text.CompareTag("TextGerman") && LanguageSelect.isEnglish)) text.enabled = false;
   try {
    if (FT("Settings") != null) FT("Settings").SetActive(false);
    if (F<PlayerMovement>().Menu != null) F<PlayerMovement>().Menu.SetActive(false);
   } catch {}
  }
 }
}