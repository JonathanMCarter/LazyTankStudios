
using UnityEngine;

public class PlayerIceDetection : A
{
    PlayerMovement MyPlayer;
    Rigidbody2D myRigid;
    void Start()
    {
        MyPlayer = FindObjectOfType<PlayerMovement>();
        myRigid = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ice"))
    //    {
    //       // if (myRigid.velocity.x < 0.1 && myRigid.velocity.x > -0.1 && myRigid.velocity.y < 0.1 && myRigid.velocity.y > -0.1)
    //       if (myRigid.velocity.magnitude == 0) //temp added by LC, may do same thing as above line, or may not work
    //        {
    //            MyPlayer.onIce = false;
    //        }
    //        else
    //            MyPlayer.onIce = true;
    //      //  Debug.Log("OnIce");
    //    }
   // }
}
