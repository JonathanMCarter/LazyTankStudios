using UnityEngine;
using UnityEngine.Events;

/*
 * Script for objects that need to trigger certain events
 * 
 *  manages what should happen if triggerplate is activated
 *
 * Owner: Andreas Kraemer
 * Last Edit : 7/10/19
 * 
 * Also Edited by : Lewis Cleminson
 * Last Edit: 07.10.19
 * Reason: Disabled push script on boulder after it reaches trigger
 * 
 * */

public class TiggerPlate : A
{
    public UnityEvent trigger;

    //


    void OnTriggerEnter2D(Collider2D collision)
    {
                if(collision.gameObject.layer==8)
        {
            trigger.Invoke();
            collision.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            Destroy(collision.GetComponent<PushObject>());//Added by LC
        }
    }


}
