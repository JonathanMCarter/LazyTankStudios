using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Quest Script - For POC
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 8/10/19

*/

public class QuestScript : MonoBehaviour
{

    public QuestItem CurrentQuest;

    public int Stage;
    public int QuestMaxStage = 10;

    public Text DisplayText;
    public Text QuestComplete;

    public GameObject LastQG;

    public void StartQuest()
    {
        if (Stage == 0)
        {
            Stage = 1;
            DisplayText.text = CurrentQuest.TaskText[0];
            LastQG = gameObject;
        }
        else
        {
            NextStage(gameObject);
        }
    }

    public void NextStage(GameObject Go)
    {
        if (Go != LastQG)
        {
            if (Stage > 0)
            {
                if (Stage + 1 < QuestMaxStage)
                {
                    Stage++;
                    DisplayText.text = CurrentQuest.TaskText[Stage - 1];
                    LastQG = Go;
                }
            }
        }
    }
}
