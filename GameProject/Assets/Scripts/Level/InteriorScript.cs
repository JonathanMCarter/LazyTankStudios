using UnityEngine;
using UnityEngine.SceneManagement;
public class InteriorScript: A {
 public enum Modes {
  Internal,
  Scene,
 };
 public Modes Type;
 public string SceneName;
 public GameObject destination;
 public bool inside = false;
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            switch (Type) {
                case Modes.Internal:
                    if (!inside) {
                        collision.transform.position = destination.transform.position;
                        G<InteriorScript>(destination).inside = true;
                    }
                    break;
                case Modes.Scene:
                    if (SceneName.Length != 0) {
                        SceneManager.LoadScene(SceneName);
                        GameObject.FindObjectOfType<SaveSys>().Save();
                    }
                    break;
                default:
                    break;
            }
        }
 }
 void OnTriggerExit2D(Collider2D collision) {
  inside = false;
 }
}