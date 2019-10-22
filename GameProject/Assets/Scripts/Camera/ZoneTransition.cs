using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Created by Toby Wishart
 * Last edit: 20/10/19
 * 
 * Script for fading in and out with the gate
 */
public class ZoneTransition : MonoBehaviour
{

    Animator a;
    public Transform destination;
    GameObject player;

    void Start()
    {
        a = GameObject.Find("ZoneFadeScreen").GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerZoneTransition>().Destination = destination;
        Camera.main.GetComponent<CameraMove>().enabled = false;
        a.SetBool("in", false);
        a.SetBool("out", true);
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
