using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<QuestLog>().SetQuest();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
