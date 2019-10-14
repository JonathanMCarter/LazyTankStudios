using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    /* 
     * Vendor Script
     * 
     * Created by: Gabriel Potamianos
     * Last edited: 14/10/2019
     * 
     * It does retrieve the file dialogue and connects it to the dialogue handler.
     * 

    */
    //File to get Dialgoue
    public DialogueFile VendorSpeech;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
            GameObject.Find("DialogueHandler").GetComponent<DialogueScript>().ChangeFile(VendorSpeech);

    }
}
