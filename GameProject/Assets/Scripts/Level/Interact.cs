using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
*Edit by: Andreas Kraemer
*
* added check for player tag so interact is only invoked if the player is colliding with the object 
* instead of any collision being able to trigger the invoke if Fire1 is pressed
*
* changed triggerstay to collisionstay as the interactable objects use colliders
 */

public class Interact : MonoBehaviour
{

    public UnityEvent interact;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetButtonDown("Fire1")&&collision.gameObject.tag=="Player")
        {
            interact.Invoke();
        }
    }

}
