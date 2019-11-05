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
 * Owner: Toby Wishart
 * Last Edit : 29/10/19
 * 
 * Also Edited by : Lewis Cleminson
 * Last Edit: 07.10.19
 * Reason: Optimize finding object
 * 
 * Also Edited by : Andreas Kraemer
 * Last Edit: 04.11.19
 * Reason: Dialogue sound effect
 * */

    public GameObject panel;
    public DialogueFile dialogue;
    public DialogueFile questdialogue;
    public PlayerMovement movement;
    DialogueScript ds;
    //QuestScript qs;
    //Andreas edit --
    private AudioManager audioManager;
    //Andreas edit end--

    void Awake()
    {
        ds = FindObjectOfType<DialogueScript>(); //added by LC
        //Andreas edit --
         audioManager=FindObjectOfType<AudioManager>();
        //Andreas edit end--
        //panel = FindObjectOfType<DialogueBoxManager>().gameObject;
        //qs = FindObjectOfType<QuestScript>();
        //ds = GameObject.Find("DialogueHandler").GetComponent<DialogueScript>();
        ds.ChangeFile(dialogue);

        // Jonathan Edit
        movement = FindObjectOfType<PlayerMovement>();
    }

    bool talking = false;
    bool FileRead = false;



    public void talk()
    {
        Debug.Log("Talk Called");
        panel.SetActive(true);

        //Toby: Need this to change the dialogue each time you talk otherwise every NPC will have the same dialogue
        ds.ChangeFile(dialogue);
        ds.Input();
        if (!ds.FileHasEnded && audioManager != null) audioManager.Play("Dialogue1");
        // ds.Reset();
        talking = true;
        movement.enabled = !talking;
    }

    void Update()
    {
        if (talking)
        {
            if (Input.GetButtonDown("Fire1"))
            { 
                ds.Input();
                //Andreas edit --
               if (!ds.FileHasEnded && !ds.InCinematic) audioManager.Play("Dialogue1"); //edited by Lc to play audio at start of dialog
                //Andreas edit end--
            }

            ///////////////// comment from LC - This set of code is running every frame that the user is "talking". Does this need to happen every frame or should it happen once at start of dialog and again at end of dialog
            talking = !ds.FileHasEnded;
            panel.SetActive(talking);
            movement.enabled = !talking;
            FileRead = true;
            //////////////////////

        }
    }
}
