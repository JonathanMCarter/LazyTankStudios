using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIceDetection : MonoBehaviour
{
    PlayerMovement MyPlayer;
    Rigidbody2D myRigid;
    void Start()
    {
        MyPlayer = FindObjectOfType<PlayerMovement>();
        myRigid = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            if (myRigid.velocity.x < 0.1 && myRigid.velocity.x > -0.1 && myRigid.velocity.y < 0.1 && myRigid.velocity.y > -0.1)
            {
                MyPlayer.onIce = false;
            }
            else
                MyPlayer.onIce = true;
            Debug.Log("OnIce");
        }
    }
}
