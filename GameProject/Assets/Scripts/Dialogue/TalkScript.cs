using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkScript : MonoBehaviour
{
    public GameObject panel;
    public DialogueFile dialogue;
    public PlayerMovement movement;
    DialogueScript ds;

    void Start()
    {
        ds = GameObject.Find("DialogueHandler").GetComponent<DialogueScript>();
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
