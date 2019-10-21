using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
 * Last Edit: 17.10.19
 * Reason: Link Heart UI and attack animation
 * 
 * */

public class PlayerMovement : MonoBehaviour
{
    public float speed = 100;
    private float baseSpeed;//for when the speed needs to be changed temporarily
    public Inventory i;

    // Added by Jonathan
    public GameObject DamageBulletThingy;
    public ProjectileStats WeaponStats;
    // end of edit here...

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
        myRigid = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        IM = FindObjectOfType<InputManager>();
        //Tony Was here
        attackHitBox.gameObject.SetActive(false);
        attacking = false;
        baseSpeed = speed;
        //Tony Left Start

        //Andreas edit--
        healthUI.maxHealth=health;
        healthUI.currentHealth=health;
        healthUI.ShowHearts();//tony addition
        //Andreas edit end--
    }

    void Update()
    {
        //Debug.DrawLine(transform.position, transform.right, Color.yellow);

        myRigid.velocity = new Vector2(IM.X_Axis(), IM.Y_Axis()) * Time.deltaTime * speed;

        //tony function
        //SetRotater();//rotates the attack hit box

        myAnim.SetFloat("SpeedX", Mathf.Abs(IM.X_Axis()));
        myAnim.SetFloat("SpeedY", IM.Y_Axis());
        if (IM.Button_Menu())
        {
            i.open();
            this.enabled = false;
        }
        //Toby: A and B item actions
        if (IM.Button_A() && !i.isOpen)
        {
            Debug.Log("Test");
            useItem(i.equippedA);
        }

        if (IM.Button_B() && !i.isOpen)
        {
            useItem(i.equippedB);
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

        Debug.Log(ID.ToString());

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
                if (attacking) attacking = false;
                //Debug.Log("Trying to use nothing");
                break;
        }
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


    // Added by Jonathan
    public void FireProjectile()
    {
        //Debug.Log(((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized));
        GameObject Go = Instantiate(DamageBulletThingy, transform.position, transform.rotation);
        Go.transform.localScale = new Vector3(WeaponStats.Size, WeaponStats.Size, WeaponStats.Size);

        // I'm aware that if you click close to yourself the bullet goes slow. 
        Vector3 Dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        Go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Clamp(Dir.x, -.3f, .3f), Dir.y) * WeaponStats.Speed, ForceMode2D.Impulse);
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
        Debug.LogError("this is running");
        health-=damage;
        healthUI.currentHealth=health;
        if (health <= 0) gameObject.SetActive(false);  
    }
    
    
    //rotates the attackhitbox with the player
    //private void SetRotater()
    //{
    //    //Tony - moves attack location 
    //    if (myRigid.velocity.x > 0.1)
    //    {
    //        myRenderer.flipX = false;
    //        attackRotater.SetPositionAndRotation(new Vector2(attackRotater.position.x, attackRotater.position.y), new Quaternion(0, 0, 0, 0));
    //    }
    //    //was having a problem with this so it might look kinda weird
    //    if (myRigid.velocity.x < -0.1)
    //    {
    //        myRenderer.flipX = true;
    //        attackRotater.SetPositionAndRotation(new Vector2(attackRotater.position.x, attackRotater.position.y), new Quaternion(0, 0, 180, 0));
    //    }
    //    if (myRigid.velocity.y > 0.1)
    //    {
    //        attackRotater.SetPositionAndRotation(new Vector2(attackRotater.position.x, attackRotater.position.y), new Quaternion(0, 0, 0, 0));
    //        if (attackRotater.rotation != new Quaternion(0, 0, 90, 0))
    //            attackRotater.Rotate(Vector3.forward * 90);
    //    }
    //    if (myRigid.velocity.y < -0.1)
    //        if (attackRotater.rotation != new Quaternion(0, 0, -90, 0))
    //            attackRotater.Rotate(Vector3.forward * -90);
    //}
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
