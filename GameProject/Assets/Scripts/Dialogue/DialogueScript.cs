using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DialogueScript: A {
 bool InC = false;
 public DialogueFile File;
 public Text DialName;
 public Text DialText;
 int DS = 0;
 bool IsCoR;
 public bool InputPressed;
 public bool RequireInput = true;
 public bool FileHasEnded = false;
 public int TypeWriterCount = 1;
 public int TypeWriterCharactersToAdvanceBy = 1;
 Coroutine PCo;
 public Animator AnimToPlay;
 void Update() {
  if (RequireInput) {
   if ((!IsCoR) && (InputPressed) && !FileHasEnded) {
    DNL();
   }
  }
 }
 public void ChangeFile(DialogueFile I) {
  if (I) {
   File = I;
  }
  R();
 }
 public void DNL() {
  if (DS < File.Names.Count) {
   switch (File.Names[DS]) {
    case "###":
     DialName.text = "";
     DialText.text = "";
     if (PCo == null) {
      PCo = SC(CL(File.Dialogue[DS]));
     }
     break;
    case "@@@":
     if (PCo == null) {
      PCo = SC(PD(3));
     }
     break;
    case "^^^":
     AnimToPlay.Play(File.Dialogue[DS], -1);
     break;
    case "***":
     DS = 0;
     FileHasEnded = true;
     break;
    default:
     DialName.text = File.Names[DS];
     DialText.text = File.Dialogue[DS];
     DS++;
     InputPressed = false;
     break;
   }
  } else {
   DialName.text = "";
   DialText.text = "";
   FileHasEnded = true;
  }
 }
 public void Input() {
  if (!InputPressed) {
   InputPressed = true;
  }
 }
 void R() {
  if (InputPressed) {
   InputPressed = false;
  }
  if (FileHasEnded) {
   FileHasEnded = false;
  }
  DS = 0;
 }
 IEnumerator PD(float D) {
  yield return new WaitForSeconds(D);
  ++DS;
 }
IEnumerator CF(string c) {
  InC = true;
  Scene s = SceneManager.GetSceneByName(c);
  yield return new WaitWhile(() => s.isLoaded);
  InC = false;
  DS++;
  PCo = null;
 }
IEnumerator CL(string c) {
  SceneManager.LoadSceneAsync(c, LoadSceneMode.Additive);
  Scene s = SceneManager.GetSceneByName(c);
  yield return new WaitWhile(() => !s.isLoaded);
  PCo = SC(CF(c));
 }
}