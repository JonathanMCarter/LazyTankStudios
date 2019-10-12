using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * Created by: Toby Wishart
*Edit by: Andreas Kraemer

*
* added check for player tag so interact is only invoked if the player is colliding with the object 
* instead of any collision being able to trigger the invoke if Fire1 is pressed
* 
* * *Edit by: Jonathan Carter - made it on enter and not need an input so it will register for the POC
*
* changed triggerstay to collisionstay as the interactable objects use colliders
 */

public class Interact : MonoBehaviour
{

    public UnityEvent interact;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            interact.Invoke();
        }
    }

}
