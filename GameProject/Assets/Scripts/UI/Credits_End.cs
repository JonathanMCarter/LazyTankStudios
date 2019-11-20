using UnityEngine;
using UnityEngine.SceneManagement;
public class Credits_End: StateMachineBehaviour {
 override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
  SceneManager.LoadScene("Main Menu");
 }
}