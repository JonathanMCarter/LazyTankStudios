﻿using System.Collections;
using System.Collections.Generic;
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
 * Also Edited by : <Enter name here if you edit this script>
 * Last Edit: <Date here if you edit this script>
 * 
 * */

public class TiggerPlate : MonoBehaviour
{
    public UnityEvent trigger;

    //
    void  OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.layer==8)
        {
            trigger.Invoke();
            otherCollider.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
        }
    }
}
