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
 * Also Edited by : Andreas Kraemer
 * Last Edit: 17.10.19
 * Reason: Link Heart UI and attack animation
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

    //Edit by Andreas--
    //public SpriteRenderer[] Hearts;
    public HealthUI healthUI; 
    //Andreas edit end--

    public int health;
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
        //Andreas edit--
        //health = 3;
        //Andreas edit end--
        attackHitBox.gameObject.SetActive(false);
        attacking = false;
        //Tony Left Start

        //Andreas edit--
        healthUI.maxHealth=health;
        healthUI.currentHealth=health;
        //Andreas edit end--
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
            //Andreas edit
            //PlayKickAnimation();
            PlayAttackAnimation();
            //Andreas edit end
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

    ///<summary>
    ///Plays the Hero's Kick animation
    ///</summary>
    public void PlayKickAnimation()
    {
        myAnim.Play("Hero_Kick", 0);
    }

    ///<summary>
    ///Plays the Hero's Attack animation
    ///</summary>
    public void PlayAttackAnimation()
    {
        myAnim.SetTrigger("Attack");
    }

    private void OnDisable()
    {
        myRigid.velocity = new Vector2(0, 0);
    }

    //Tony Was Here too--------------------------------------
    //Edit Andreas--
    /* 
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
    */
    /* 
    public void RemoveHeart()
    {
        Hearts[health - 1].gameObject.SetActive(false); 
    }
    */
    /*
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //RemoveHeart();
            --health;
            healthUI.currentHealth=health;
            if (health <= 0) gameObject.SetActive(false);             
        }
    }
    */
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage(1);           
        }
    }

    ///<summary>
    /// Cause the player to take desired amount of damage
    ///</summary>
    public void TakeDamage(int damage)
    {
        health-=damage;
        healthUI.currentHealth=health;
        if (health <= 0) gameObject.SetActive(false);  
    }

    /* 
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("AI isn't my job");
            for (int i = 0; i < health; ++i)
                Hearts[i].gameObject.SetActive(false);
        }
    }
    */
    //Andreas edit end--
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
