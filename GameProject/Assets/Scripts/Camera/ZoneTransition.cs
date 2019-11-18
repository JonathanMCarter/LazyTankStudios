
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * Created by Toby Wishart
 * Last edit: 03/11/19
 * 
 * Script for fading in and out with the gate
 */
public class ZoneTransition : A
{
    public string destination;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
             FindObjectOfType<PlayerZoneTransition>().StartTransition(destination);
        }
    }

}
