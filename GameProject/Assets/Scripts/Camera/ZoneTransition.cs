using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//added by LC

/**
 * Created by Toby Wishart
 * Last edit: 03/11/19
 * 
 * Script for fading in and out with the gate
 */
public class ZoneTransition : MonoBehaviour
{

    Animator a;
    public string destination;
    GameObject player;

    void Start()
    {
        a = GameObject.Find("ZoneFadeScreen").GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerZoneTransition>().Destination = destination;
            //Camera.main does not work here
            GameObject.Find("Main Camera").GetComponent<CameraMove>().enabled = false;
            a.SetBool("in", false);
            a.SetBool("out", true);
        }
    }

    //Function for fade out animation event
    public void FadeOutEnd()
    {
        player.GetComponent<PlayerZoneTransition>().StartTransition();

    }

    //Function for fade in animation event
    public void FadeInEnd()
    {
        a.SetBool("in", false);
        a.SetBool("out", false);
        player.GetComponent<PlayerMovement>().enabled = true;

    }

}
