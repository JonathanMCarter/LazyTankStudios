using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sign : A
{
    TalkScript ts;
    DialogueScript ds;
    // Start is called before the first frame update
    void Start()
    {
        ts = GetComponent<TalkScript>();
        ds = F<DialogueScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ts.isItTalking())
        {
            ds.Input();
        }
    }
}
