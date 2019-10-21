﻿using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Player Movement Script
 * 
 * This script gets the input using the Unity Input manager for K/M and controller input, as well as touch data and outputs it using 3 public functions. This allows other scripts to access input data from across all platforms using one function.
 * 
 * Owner: Toby Wishart (but really is Lewis') 
 * Last Edit : 19/10/19
 * Reason: Integrated items
 * 
 * Also Edited by : Tony Parsons
 * Last Edit: 13.10.19
 * Reason: Combat
 * 
 * Also Edited by : Andreas Kraemer
 * Last Edit: 21.10.19
 * Reason: Call Sound Effects
 * 
 * Also Edited by: Lewis Cleminson
 * Last Edit: 21.10.19
 * Reason: HealthUI null reference
 * Added Enum for facing direction for bullets direction reference
 * 
 * */

public class PlayerMovement : MonoBehaviour
{
    private enum Direction {Up, Down, Left, Right };
    private Direction ImFacing;
    public float speed = 100;
    private float baseSpeed;//for when the speed needs to be changed temporarily
  //  public Inventory i; //please dont name variables as one letter. we need to be able to clearly know what something is atm. Comment added by LC
    public Inventory myInventory;
    // Added by Jonathan
    public GameObject DamageBulletThingy;
    public ProjectileStats WeaponStats;
    // end of edit here...

    //Start of Update from LC
    private Rigidbody2D myRigid;
    private Animator myAnim;
    private SpriteRenderer myRenderer;
    private InputManager IM;
    public bool TakeDamageCD; //temp add by LC
    //Tony Was Here

    //Edit by Andreas--
    //public SpriteRenderer[] Hearts;
    public HealthUI healthUI;
    private AudioManager audioManager;
    //Andreas edit end--

    //Tony's Variables
    public int health;
    public BoxCollider2D attackHitBox;//atach to attackrotater
    public Transform attackRotater;//make a new transform as a child of the hero, make the attackHitBox a child of this transform
    private bool attacking;
    public float AbilityDuration;
    private float countdown;
    public float dashSpeedMultiplier;
    private bool dashing;
    private bool shieldUp;
    //end of Tony variables

    //Items enum, should matchup with the item IDs i.e. Sword is in slot 0 and has ID 0 therefore SWORD is 0 here
    enum ITEMS
    {
        SWORD, BLAZBOOTS, ICEBOW, SHIELDSHARPTON, TELERUNE, ELIXIRLIFE, ELIXIRSTR
    }

    private void Start()
    {
        ImFacing = Direction.Down; //Added by LC
        myRigid = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        IM = FindObjectOfType<InputManager>();

        attackHitBox = transform.GetChild(0).GetComponent<BoxCollider2D>(); //added by LC
        //Tony Was here
        attackHitBox.gameObject.SetActive(false);
        attacking = false;
        baseSpeed = speed;
        //Tony Left Start

        if (healthUI == null) healthUI = FindObjectOfType<HealthUI>(); //Added by LC

        //Andreas edit--
        healthUI.maxHealth=health;
        healthUI.currentHealth=health;
        healthUI.ShowHearts();//tony addition
        //Andreas edit end--

        //Andreas edit--
        audioManager=GameObject.FindObjectOfType<AudioManager>();
        //Andreas edit end--
    }

    void Update()
    {
        //Debug.DrawLine(transform.position, transform.right, Color.yellow);

        myRigid.velocity = new Vector2(IM.X_Axis(), IM.Y_Axis()) * Time.deltaTime * speed; //Changing a rigidbody should take place in FixedUpdate rather than Update as that is when the physics system runs, whereas update is when items are drawn on screen. comment added by LC

        //tony function
        SetRotater();//rotates the attack hit box

        myAnim.SetFloat("SpeedX", Mathf.Abs(IM.X_Axis()));
        myAnim.SetFloat("SpeedY", IM.Y_Axis());

        /////////////////////Added by LC as temp for directions////////////////////
        if (IM.X_Axis() > 0.1f) ImFacing = Direction.Right;
        if (IM.X_Axis() < -0.1f) ImFacing = Direction.Left;
        if (IM.Y_Axis() > 0.1f) ImFacing = Direction.Up;
        if (IM.Y_Axis() < -0.1f) ImFacing = Direction.Down;

        ////////////////////////////////////////////////

        if (IM.Button_Menu())
        {
            myInventory.open();
            this.enabled = false;
        }
        //Toby: A and B item actions
        if (IM.Button_A() && !myInventory.isOpen)
        {
           // Debug.Log("Test"); //commented out by LC to help clear debug log
            useItem(myInventory.equippedA);
        }

        if (IM.Button_B() && !myInventory.isOpen)
        {
            useItem(myInventory.equippedB);
        }


        //Tony action stuff
        if (attacking || dashing || shieldUp)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                if (attacking)
                {
                    attackHitBox.gameObject.SetActive(false);
                    attacking = false;
                }
                if (dashing)
                {
                    speed = baseSpeed;
                    dashing = false;
                }
                if (shieldUp)
                    shieldUp = false;
            }
        }
        //To here
        //To here
    }
    //Toby: Function for using an item of a given ID
    void useItem(int ID)
    {
        //setup countdown
        countdown = AbilityDuration;


        //Debug.Log(ID.ToString());//commented out by LC to help clear debug log

        switch (ID)
        {
            case (int)ITEMS.SWORD:
                //Andreas edit
                //PlayKickAnimation();
                PlayAttackAnimation();
                //Andreas edit end
                attackHitBox.gameObject.SetActive(true);
                attacking = true;

                // Jonathan Added This function
                FireProjectile();
                break;

            //Tony Stuff
            case (int)ITEMS.BLAZBOOTS:
                //animation here

                //-----------
                speed *= dashSpeedMultiplier;
                dashing = true;
                break;
            case (int)ITEMS.SHIELDSHARPTON:
                //animation
                //---------
                shieldUp = true;
                break;
            // Temp - just so the axe or anything with the ID of 1 works for now.....
            case 4:
                //Andreas edit
                //PlayKickAnimation();
                PlayAttackAnimation();
                //Andreas edit end
                attackHitBox.gameObject.SetActive(true);
                attacking = true;

                // Jonathan Added This function
                FireProjectile();
                break;
            case -1:
            default:
                //nothing or invalid item equipped
                FireProjectile(); //Added by LC for testing purposes
                if (attacking) attacking = false;
                //Debug.Log("Trying to use nothing");
                break;
        }
    }


    //Andreas edit --Animation has been removed
    /* 
    public void PlayKickAnimation()
    {
        myAnim.Play("Hero_Kick", 0);
    }
    */
    //

    ///<summary>
    ///Plays the Hero's Attack animation
    ///</summary>
    public void PlayAttackAnimation()
    {
        myAnim.SetTrigger("Attack");
    }


    //// Added by Jonathan
    //public void FireProjectile()
    //{
    //    Debug.Log(((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized));
    //    GameObject Go = Instantiate(DamageBulletThingy, transform.position, transform.rotation);

    //    //I'm aware that if you click close to yourself the bullet goes slow. 
    //    Vector3 Dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

    //    Go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Clamp(Dir.x, -.3f, .3f), Dir.y) * WeaponStats.Speed, ForceMode2D.Impulse);

    //    //Alternate firing method - Tony (comment above uncomment this) In terms of input I think there are only 3 buttons in the final build, unless I missed something
    //    //GameObject Go = Instantiate(DamageBulletThingy, attackHitBox.transform.position, attackRotater.transform.rotation);
    //    //Go.transform.localScale = new Vector3(WeaponStats.Size, WeaponStats.Size, WeaponStats.Size);
    //    //--------------------------------
    //    Go.GetComponent<Rigidbody2D>().velocity = Go.transform.right * WeaponStats.Speed;
    //    Destroy(Go, WeaponStats.Lifetime);
    //}
    
    //LC remade function - Cant set force of bullet by mouse position, as this wont work with joystick input or mobile input. Instead need to find which way player is facing and fire in that direction


    public void FireProjectile() //Fires off player facing certain direction
    {
        GameObject Go = Instantiate(DamageBulletThingy, attackRotater.GetChild(0).transform.position, transform.rotation);
        Vector2 Dir = new Vector2(0, 0);
        switch (ImFacing)
        {
            case (Direction.Down):
                Dir.y = -1f; //why 3?
                break;
            case (Direction.Left):
                Dir.x = -1f;
                break;
            case (Direction.Right):
                Dir.x = 1f;
                break;
            case (Direction.Up):
                Dir.y = 1f;
                break;
            default:
                print("Direction Error - LC");
                break;
        };
        Go.GetComponent<Rigidbody2D>().AddForce(Dir * WeaponStats.Speed, ForceMode2D.Impulse);
        Destroy(Go, WeaponStats.Lifetime);
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
            Debug.LogError("this is running");
            //Debug.Log("********** Player Should Be Taking Damage Now...");

            TakeDamage(1);
            // other.gameObject.GetComponent<AIMovement>().Health = 0;
        }
    }

    ///<summary>
    /// Cause the player to take desired amount of damage
    ///</summary>
    public void TakeDamage(int damage)
    {

        //added by LC to stop taking damage too quickly
        if (TakeDamageCD) return;
        if (health <= 0) return;
        StartCoroutine(DamageCooldown());


        //Debug.LogError("this is running");
        health-=damage;
        healthUI.currentHealth=health;
        healthUI.ShowHearts(); //update the display of hearts. LC
        audioManager.Play("Damage");
        if (health <= 0)
        {
            audioManager.Play("Death");
            StartCoroutine(GameReset());
            //gameObject.SetActive(false); 
            this.enabled = false;
            
        } 
    }

    IEnumerator GameReset() //added temp by LC
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator DamageCooldown() //temp add by LC
    {
        TakeDamageCD = !TakeDamageCD;
        yield return new WaitForSeconds(1);
        TakeDamageCD = !TakeDamageCD;
    }

    ///<summary>
    /// Heals the player for [value]
    ///</summary>
    public void Heal(int value)
    {
        health=health+value<healthUI.maxHealth?health+value:healthUI.maxHealth;
        healthUI.currentHealth=health;
        audioManager.Play("Heal");
    }


    //rotates the attackhitbox with the player
    private void SetRotater()
    {
        //Tony - moves attack location 
        if (myRigid.velocity.x > 0.1)
        {
            myRenderer.flipX = false;
            attackRotater.SetPositionAndRotation(new Vector2(attackRotater.position.x, attackRotater.position.y), new Quaternion(0, 0, 0, 0));
            
        }
        //was having a problem with this so it might look kinda weird
        if (myRigid.velocity.x < -0.1)
        {
            myRenderer.flipX = true;
            attackRotater.SetPositionAndRotation(new Vector2(attackRotater.position.x, attackRotater.position.y), new Quaternion(0, 0, 180, 0));
        }
        if (myRigid.velocity.y > 0.1)
        {
            attackRotater.SetPositionAndRotation(new Vector2(attackRotater.position.x, attackRotater.position.y), new Quaternion(0, 0, 0, 0));
            if (attackRotater.rotation != new Quaternion(0, 0, 90, 0))
                attackRotater.Rotate(Vector3.forward * 90);
        }
        if (myRigid.velocity.y < -0.1)
            if (attackRotater.rotation != new Quaternion(0, 0, -90, 0))
                attackRotater.Rotate(Vector3.forward * -90);
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
