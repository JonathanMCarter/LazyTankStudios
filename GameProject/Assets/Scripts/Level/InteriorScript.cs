using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Created by Toby Wishart
 * Last edit: 11/10/19
 * 
 * Last edit: Jonathan @ 14:07 ish - 21/10/19
 * added modes so the player can change scene
 */
public class InteriorScript : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        inside = false;
    }

}
