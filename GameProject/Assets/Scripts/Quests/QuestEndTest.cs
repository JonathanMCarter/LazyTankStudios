using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Made by jonathan as a test thingy to show the quests working...
 * 21/10/19
 * 
 * 
 * 
 */ 
public class QuestEndTest : MonoBehaviour
{
    public Quest QST;
    public Quests QS;

    public DialogueScript DS;
    public DialogueFile File;
    
    //Andreas - add sound effect
    private AudioManager audioManager;


    private void Start()
    {
        QS = FindObjectOfType<Quests>();
        DS = FindObjectOfType<DialogueScript>();
        QST = QS.Quest;
        //Andreas - add sound effect
        audioManager=GameObject.FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (QST.status == Quest.Status.OnGoing)
        {
            QST.status = Quest.Status.Completed;
            DS.ChangeFile(File);
            Debug.Log("quest done?");
            //Andreas - add sound effect
            //audioManager.Play("Victory");
        }
    }
}
