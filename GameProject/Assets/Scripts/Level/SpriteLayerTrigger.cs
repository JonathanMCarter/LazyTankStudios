using UnityEngine;

/*
 * Sprite Layer sort Script 
 * 
 *  this script is used to change the sprite sort order of anything that enters the attached trigger to be behind the current object for the 2.5D illusion
 *
 * Owner: Andreas Kraemer
 * Last Edit : 1/10/19
 * 
 * Also Edited by : <Enter name here if you edit this script>
 * Last Edit: <Date here if you edit this script>
 * 
 * */

public class SpriteLayerTrigger : A
{
    int OriginalSortingLayer=10;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.tag=="Player"){
        OriginalSortingLayer=otherCollider.GetComponent<SpriteRenderer>().sortingOrder;
        otherCollider.gameObject.GetComponent<SpriteRenderer>().sortingOrder=0;
        }
    }
    void OnTriggerExit2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.tag=="Player")
        otherCollider.gameObject.GetComponent<SpriteRenderer>().sortingOrder=OriginalSortingLayer;
    }
}
