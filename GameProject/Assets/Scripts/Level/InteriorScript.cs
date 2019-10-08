using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorScript : MonoBehaviour
{
    public GameObject destination;
    [HideInInspector]
    public bool inside = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !inside)
        {
            collision.transform.position = destination.transform.position;
            destination.GetComponent<InteriorScript>().inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inside = false;
    }

}
