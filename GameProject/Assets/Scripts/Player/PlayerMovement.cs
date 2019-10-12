using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player Movement Script
 * 
 * This script gets the input using the Unity Input manager for K/M and controller input, as well as touch data and outputs it using 3 public functions. This allows other scripts to access input data from across all platforms using one function.
 * 
 * Owner: Toby Wishart (but really is Lewis')
 * Last Edit : 
 * 
 * Also Edited by : Lewis Cleminson
 * Last Edit: 05.10.19
 * Reason: Update script to use the new Input Manager script to allow cross platform input.
 * Also each Update the script was getting 6 components which is not optimal. Changed it to get components once at start and call on them, which uses less processing.
 * 
 * */

public class PlayerMovement : MonoBehaviour
{
    public float speed = 100;
    public GameObject menu;


    //Start of Update from LC
    private Rigidbody2D myRigid;
    private Animator myAnim;
    private SpriteRenderer myRenderer;
    private InputManager IM;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        IM = FindObjectOfType<InputManager>();
    }

    void Update()
    {
       myRigid.velocity = new Vector2(IM.X_Axis(), IM.Y_Axis()) * Time.deltaTime * speed;
       myRenderer.flipX = (myRigid.velocity.x < 0);
       myAnim.SetFloat("SpeedX", Mathf.Abs(IM.X_Axis()));
       myAnim.SetFloat("SpeedY",IM.Y_Axis());
        if (IM.Button_Menu())
        {
            menu.SetActive(true);
            this.enabled = false;
        }
    }

    public void PlayKickAnimation()
    {
        myAnim.Play("Hero_Kick",0);
    }

    private void OnDisable()
    {
        myRigid.velocity = new Vector2(0, 0);
    }

    //End of update from LC

    //void Update()
    //{
    //    GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed);
    //    FlipSprite(GetComponent<Rigidbody2D>().velocity.x);
    //    GetComponent<Animator>().SetFloat("SpeedX",Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    //    GetComponent<Animator>().SetFloat("SpeedY",Input.GetAxisRaw("Vertical"));
    //    if (Input.GetButtonDown("Fire2"))
    //    {
    //        menu.SetActive(true);
    //        this.enabled = false;
    //    }
    //}

    //private void OnDisable()
    //{
    //    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    //}
    //private void FlipSprite(float velocityX)
    //{
    //    if(velocityX<0)GetComponent<SpriteRenderer>().flipX=true;
    //    else if(velocityX>0)GetComponent<SpriteRenderer>().flipX=false;
    //}
}
