using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLoad : A
{
    void Start()
    {
        F<QuestLog>().SetQuest();
    }
}
