using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DialogueScript: A {
 public bool InCinematic = false;
 public DialogueFile File;
 public Text DialName;
 public Text DialText;
 int DialStage = 0;
 bool IsCoRunning;
 public bool InputPressed;
 public bool RequireInput = true;
 public bool FileHasEnded = false;
 public int TypeWriterCount = 1;
 public int TypeWriterCharactersToAdvanceBy = 1;
 Coroutine PauseCo;
 public Animator AnimToPlay;
 void Update() {
  if (RequireInput) {
   if ((!IsCoRunning) && (InputPressed) && !FileHasEnded) {
    DisplayNextLine();
   }
  }
 }
 public void ChangeFile(DialogueFile Input) {
  if (Input) {
   File = Input;
  }
  Reset();
 }
 public void DisplayNextLine() {
  if (DialStage < File.Names.Count) {
   switch (File.Names[DialStage]) {
    case "###":
     DialName.text = "";
     DialText.text = "";
     if (PauseCo == null) {
      PauseCo = SC(CinematicLoad(File.Dialogue[DialStage]));
     }
     break;
    case "@@@":
     if (PauseCo == null) {
      PauseCo = SC(PauseDial(3));
     }
     break;
    case "^^^":
     AnimToPlay.Play(File.Dialogue[DialStage], -1);
     break;
    case "***":
     DialStage = 0;
     FileHasEnded = true;
     break;
    default:
     DialName.text = File.Names[DialStage];
     DialText.text = File.Dialogue[DialStage];
     DialStage++;
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
 public void Reset() {
  if (InputPressed) {
   InputPressed = false;
  }
  if (FileHasEnded) {
   FileHasEnded = false;
  }
  DialStage = 0;
 }
 IEnumerator PauseDial(float Delay) {
  yield
  return new WaitForSeconds(Delay);
  ++DialStage;
 }
IEnumerator CinematicFinish(string cinematic) {
  InCinematic = true;
  Scene s = SceneManager.GetSceneByName(cinematic);
  yield
  return new WaitWhile(() => s.isLoaded);
  InCinematic = false;
  DialStage++;
  PauseCo = null;
 }
IEnumerator CinematicLoad(string cinematic) {
  SceneManager.LoadSceneAsync(cinematic, LoadSceneMode.Additive);
  Scene s = SceneManager.GetSceneByName(cinematic);
  yield
  return new WaitWhile(() => !s.isLoaded);
  PauseCo = SC(CinematicFinish(cinematic));
 }
}