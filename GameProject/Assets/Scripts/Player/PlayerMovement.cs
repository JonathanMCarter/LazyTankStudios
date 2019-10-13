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
 * Also Edited by : Tony Parsons
 * Last Edit: 13.10.19
 * Reason: Combat
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
    //Tony Was Here
    public SpriteRenderer[] Hearts;
    private int health;
    public BoxCollider2D attackHitBox;
    private bool attacking;
    public float AttackDuration;
    private float countdown;
    //end of Tony variables

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        IM = FindObjectOfType<InputManager>();
        //Tony Was here
        health = 3;
        attackHitBox.gameObject.SetActive(false);
        attacking = false;
        //Tony Left Start
    }

    void Update()
    {
        myRigid.velocity = new Vector2(IM.X_Axis(), IM.Y_Axis()) * Time.deltaTime * speed;
        myRenderer.flipX = (myRigid.velocity.x < 0);
        myAnim.SetFloat("SpeedX", Mathf.Abs(IM.X_Axis()));
        myAnim.SetFloat("SpeedY", IM.Y_Axis());
        if (IM.Button_Menu())
        {
            menu.SetActive(true);
            this.enabled = false;
        }
        //Tony was Here
        if (IM.Button_A())//attacking
        {
            PlayKickAnimation();
            attackHitBox.gameObject.SetActive(true);
            attacking = true;
            countdown = AttackDuration;
        }
        if (attacking)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                attackHitBox.gameObject.SetActive(false);
                attacking = false;
            }
        }
        //To here
    }

    public void PlayKickAnimation()
    {
        myAnim.Play("Hero_Kick", 0);
    }

    private void OnDisable()
    {
        myRigid.velocity = new Vector2(0, 0);
    }

    //Tony Was Here too--------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            for (int i = 0; i < health; ++i)
            {
                Hearts[i].gameObject.SetActive(true);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Hearts[health - 1].gameObject.SetActive(false);
            --health;
            if (health <= 0)
                gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("AI isn't my job");
            for (int i = 0; i < health; ++i)
                Hearts[i].gameObject.SetActive(false);
        }
    }
    //Tony Has left-----------------------------------

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
