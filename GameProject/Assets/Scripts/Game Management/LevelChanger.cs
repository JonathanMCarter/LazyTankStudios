using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChanger: A {
public Animator animator;
InputManager IM;
public int levelToLoad;
void Update() {
if (Input.GetMouseButtonDown(0)) {
FadeToLevel(levelToLoad);}}
public void FadeToLevel(int levelIndex) {
levelToLoad = levelIndex;
animator.SetTrigger("FadeOut");}
public void OnFadeComplete() {
SceneManager.LoadScene(levelToLoad);}
void Start() {
IM = F<InputManager>();}}