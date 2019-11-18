using UnityEngine;
using UnityEngine.SceneManagement;
public class InteriorScript : A
{
    public enum Modes
    {
        Internal,
        Scene,
    };
    public Modes Type;
    public string SceneName;
    public GameObject destination;
    [HideInInspector]
    public bool inside = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (Type)
        {
            case Modes.Internal:
                if (collision.CompareTag("Player") && !inside)
                {
                    collision.transform.position = destination.transform.position;
                    destination.GetComponent<InteriorScript>().inside = true;
                }
                break;
            case Modes.Scene:
                if (SceneName.Length != 0)
                {
                    SceneManager.LoadScene(SceneName);
                }
                break;
            default:
                break;
        }
    }
     void OnTriggerExit2D(Collider2D collision)
    {
        inside = false;
    }
}
