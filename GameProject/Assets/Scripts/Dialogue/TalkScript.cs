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
    public DialogueFile dialogueEnglish,dialogueGerman;
    public DialogueFile questdialogue;
    public PlayerMovement movement;
    DialogueScript ds;
    //QuestScript qs;
    //Andreas edit --
    private AudioManager audioManager;
    //Andreas edit end--

    bool talking = false;
    //bool FileRead = false;

    void Awake()
    {
        ds = FindObjectOfType<DialogueScript>(); //added by LC
        //Andreas edit --
         audioManager=FindObjectOfType<AudioManager>();
        
        //panel = FindObjectOfType<DialogueBoxManager>().gameObject;
        //qs = FindObjectOfType<QuestScript>();
        //ds = GameObject.Find("DialogueHandler").GetComponent<DialogueScript>();
        if(LanguageSelect.isEnglish)ds.ChangeFile(dialogueEnglish);
        else ds.ChangeFile(dialogueGerman);
        //Andreas edit end--
        

        // Jonathan Edit
        movement = FindObjectOfType<PlayerMovement>();
    }


    public void talk()
    {
        Debug.Log("Talk Called");
        panel.SetActive(true);

        //Toby: Need this to change the dialogue each time you talk otherwise every NPC will have the same dialogue
       
        if(LanguageSelect.isEnglish)ds.ChangeFile(dialogueEnglish);
        else ds.ChangeFile(dialogueGerman);
        ds.Input();
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
                //Andreas edit end--
            }

            ///////////////// comment from LC - This set of code is running every frame that the user is "talking". Does this need to happen every frame or should it happen once at start of dialog and again at end of dialog
            talking = !ds.FileHasEnded;
            panel.SetActive(talking);
            movement.enabled = !talking;
            //FileRead = true;
            //////////////////////

        }
    }

    public bool isItTalking()
    {
        return talking;
    }
}
