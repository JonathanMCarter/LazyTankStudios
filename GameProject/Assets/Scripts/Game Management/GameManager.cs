using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager: A {
 PlayerMovement Player;
 GameObject Menu;
 void Start() {
  Player = F<PlayerMovement>();
  Menu = F("MainMenu");
 }
 public void QuitGame() {
  Application.Quit();
 }
 public void StartGame() {
  TogglePlayerMovement();
  Menu.SetActive(false);
 }
 public void ExitToMenu() {
  SceneManager.LoadScene("Main Menu");
  DoNotDes[] Gos = Fs<DoNotDes>();
  DoNotDes.Created = false;
  foreach(DoNotDes go in Gos) if (go.gameObject != gameObject) D(go.gameObject);
 }
 public void TogglePlayerMovement() {
  Player.enabled = !Player.enabled;
 }
}