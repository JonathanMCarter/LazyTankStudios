using UnityEngine;


/*
 * Script for pushable objects
 * 
 *  attach to object that should be pushed and add to the interact event
 *
 * Owner: Andreas Kraemer
 * Last Edit : 7/10/19
 * 
 * Also Edited by : <Enter name here if you edit this script>
 * Last Edit: <Date here if you edit this script>
 * 
 * */


public class PushObject : A
{
    public float pushStrenght;
    public void Push()
    {
        GetComponent<Rigidbody2D>().velocity=new Vector2(pushStrenght,0);
    }
}