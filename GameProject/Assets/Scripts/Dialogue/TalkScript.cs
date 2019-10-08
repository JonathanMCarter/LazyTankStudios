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
    public DialogueFile questdialogue;
    public PlayerMovement movement;
    DialogueScript ds;
    QuestScript qs;

    void Start()
    {
        ds = FindObjectOfType<DialogueScript>(); //added by LC
        qs = FindObjectOfType<QuestScript>();
        //ds = GameObject.Find("DialogueHandler").GetComponent<DialogueScript>();
        ds.ChangeFile(dialogue);
    }

    bool talking = false;
    bool FileRead = false;



    public void talk()
    {
        Debug.Log("Talk Called");
        panel.SetActive(true);

        //ds.Reset();
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
            FileRead = true;

        }
    }
}
