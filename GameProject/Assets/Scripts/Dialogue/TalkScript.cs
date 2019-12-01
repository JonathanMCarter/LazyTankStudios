using UnityEngine;
public class TalkScript: A {
 CanvasGroup panel;
 public DialogueFile dialogueEnglish, dialogueGerman;
 public DialogueFile questdialogue;
 public PlayerMovement movement;
 InputManager IM;
 DialogueScript ds;
 bool talking = false;
 void Awake() {
  IM = F<InputManager>();
    panel = G<CanvasGroup>(F("Dialogue Box"));
  ds = F<DialogueScript>();
  if (LanguageSelect.isEnglish) ds.ChangeFile(dialogueEnglish);
  else ds.ChangeFile(dialogueGerman);
  movement = F<PlayerMovement>();
 }
 public void talk() {
  panel.alpha = 1;
  if (LanguageSelect.isEnglish) ds.ChangeFile(dialogueEnglish);
  else ds.ChangeFile(dialogueGerman);
  ds.Input();
  talking = true;
 movement.stopInput = talking;
  movement.enabled = !talking;
 }
 void Update() {
  if (talking) {
   if (IM.Button_A()) {
    ds.Input();
   }
   talking = !ds.FileHasEnded;
   panel.alpha = talking ? 1 : 0;
movement.stopInput = talking;
   movement.enabled = !talking;
  }
 }
 public bool isItTalking() {
  return talking;
 }
}