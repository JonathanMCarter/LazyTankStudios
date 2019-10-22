using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Created by: Lewis Cleminson
 * Date: 22.10.19
 * 
 * Temp script for collecting puzzle pieces (not going to inventory)
 * 
 * 
 * */



public class PuzzleComponent : MonoBehaviour
{
    private InputManager IM;
    private bool IsCarried;
    private GameObject Player;
    static bool IsCarrying;

    private void Start()
    {
        IM = FindObjectOfType<InputManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(transform.gameObject.tag))//If tag matches that of the object
        {
            FindObjectOfType<StatuePuzzleThingy>().ItemReturn();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            IsCarrying = false;
            this.gameObject.SetActive(false);
        }
        if (IsCarried) return;
        if (collision.gameObject.CompareTag("Player") && IM.Button_A() && !IsCarrying)
        {
            Player = collision.gameObject;
            Collect();
        }
    }

    private void Update()
    {
        if (IsCarried) transform.position = Player.transform.position + new Vector3(0, 1, 0);
    }

    void Collect()
    {
        IsCarried = true;
        IsCarrying = true;

    }
}
