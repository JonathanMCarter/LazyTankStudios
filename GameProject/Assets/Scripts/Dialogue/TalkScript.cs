using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkScript : MonoBehaviour
{
    /*
 * Task Script
 * 
 *  <<Enter Description Here>>
 *
 * Owner: 
 * Last Edit : 
 * 
 * Also Edited by : Lewis Cleminson
 * Last Edit: 07.10.19
 * Reason: Optimize finding object
 * 
 * */

    public GameObject panel;
    public DialogueFile dialogue;
    public PlayerMovement movement;
    DialogueScript ds;

    void Start()
    {
        ds = FindObjectOfType<DialogueScript>(); //added by LC
        //ds = GameObject.Find("DialogueHandler").GetComponent<DialogueScript>();
    }

    bool talking = false;
    public void talk()
    {
        panel.SetActive(true);
        ds.ChangeFile(dialogue);
       
        talking = true;
        movement.enabled = !talking;
    }

    void Update()
    {
        if (talking)
        {
            if (Input.GetButtonDown("Fire1")) ds.Input();
            talking = !ds.FileHasEnded;
            panel.SetActive(talking);
            movement.enabled = !talking;
        }
    }
}
