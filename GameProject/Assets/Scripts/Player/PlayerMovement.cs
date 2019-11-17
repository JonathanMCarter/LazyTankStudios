﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : A {
    enum Dir { Up, Down, Left, Right };
    Dir F;
    public float speed = 100;
    float bS;
    public Inventory Inv;
    public GameObject Bullet, DeathCanvas, Menu;
    public ProjectileStats WeaponStats; Rigidbody2D myRigid; Animator myAnim; SpriteRenderer render; InputManager IM;
    public bool dmgCD;   HealthUI UI; SoundPlayer audioManager, BossKilled;
    public int health, QuestActiveID; GameObject aHB;
    public Transform aR;
    bool attacking, dashing, shieldUp, Shooting;
    public float RangedAttackDuration, dashSpeedMultiplier, DashDuration, blockTIme, AttackTime, SlideSpeed;
    float countdown;
    enum ITEMS { SWORD, BLAZBOOTS, ICEBOW, SHIELDSHARPTON, TELERUNE, ELIXIRLIFE, ELIXIRSTR, ELIXIRHEARTS, WATERSKIN, FURCOAT, FORESTITEM }

    private void Start()
    {
        
             if (DeathCanvas != null) DontDestroyOnLoad(DeathCanvas.gameObject);
            myRigid = GetComponent<Rigidbody2D>();
            myAnim = GetComponent<Animator>();
            render = GetComponent<SpriteRenderer>();
            IM = FindObjectOfType<InputManager>();
            UI = FindObjectOfType<HealthUI>();
            UI.maxHealth = health;
            UI.currentHealth = health;
            UI.ShowHearts();
            Menu = GameObject.FindGameObjectWithTag("OptionsMenu");
            Menu.SetActive(false);
            audioManager = FindObjectOfType<SoundPlayer>();
            aHB = aR.GetChild(0).gameObject;
            aR.gameObject.SetActive(true);
            aHB.SetActive(false); bS = speed;
        }
        void FixedUpdate()
    {
        if (countdown > 0) myRigid.velocity = new Vector2(0, 0);
        else myRigid.velocity = (new Vector2(IM.X_Axis(), IM.Y_Axis())).normalized * speed;
        
    }

    void Update()
    {
        SetRotater();

            myAnim.SetFloat("SpeedX", Mathf.Abs(IM.X_Axis()));
            myAnim.SetFloat("SpeedY", IM.Y_Axis());
            if (IM.X_Axis() > 0.1f) F = Dir.Right;
            if (IM.X_Axis() < -0.1f) F = Dir.Left;
            if (IM.Y_Axis() > 0.1f) F = Dir.Up;
            if (IM.Y_Axis() < -0.1f) F = Dir.Down;
        
        if (IM.Button_Menu())
        {
            Menu.SetActive(true);
            enabled = false;
        }

        if (IM.Button_A() && !Inv.isOpen) useItem(Inv.equippedA);
        if (IM.Button_B() && !Inv.isOpen) useItem(Inv.equippedB);
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {

                    aHB.SetActive(false);
                    aR.GetChild(1).gameObject.SetActive(false);
                    speed = bS;
                    aR.GetChild(3).gameObject.SetActive(false);
                    aR.GetChild(2).gameObject.SetActive(false);
                     myAnim.SetBool("Attack", false);

                //if (attacking)
                //{
                //    aHB.SetActive(false);
                //    attacking = false;
                //}
                //if (dashing)
                //{
                //    aR.GetChild(1).gameObject.SetActive(false);
                //    speed = bS; dashing = false;
                //}
                //if (shieldUp)
                //{
                //    aR.GetChild(3).gameObject.SetActive(false);
                //    shieldUp = false;
                //}
                //if (Shooting)
                //{
                //    aR.GetChild(2).gameObject.SetActive(false);
                //    Shooting = false;
                //}
            }
        }
    }
    void useItem(int ID)
    {
        if (countdown <= 0)
        {
            switch (ID)
            {
                case (0):
                    
                    myRigid.velocity = new Vector2(0, 0);
                    myAnim.SetBool("Attack", true);
                    countdown = AttackTime;
                    aR.GetChild(0).gameObject.SetActive(true);
                    aR.GetChild(0).gameObject.GetComponent<Bullet>().SetStats(1, new Vector2(0, 0), -1, ID);
                    //attacking = true;
                    break;

                case (1):
                    aR.GetChild(1).gameObject.SetActive(true);
                    speed *= dashSpeedMultiplier;
                    countdown = DashDuration;
                    //dashing = true;
                    break;

                case (2): aR.GetChild(2).gameObject.SetActive(true);
                   // Shooting = true;
                    countdown = RangedAttackDuration;
                    countdown = 0.3f;
                    FireProjectile(ID);
                    break;

                case (3):
                    countdown = blockTIme;
                    aR.GetChild(3).gameObject.SetActive(true);
                   // shieldUp = true;
                    break;

                case -1:
                    break;
            }
        }
    }
    //public void PlayAttackAnimation()
    //{
    //    myAnim.SetTrigger("Attack");
    //}
    public void FireProjectile(int itemUsed)
    {
        GameObject Go = Instantiate(Bullet, aR.GetChild(0).transform.position, aR.rotation * Quaternion.Euler(0, 0, -45));
        Vector2 Direc = new Vector2(0, 0);
        if (F == 0) Direc.y = 1f;
        if ((int)F == 1) Direc.y = -1f;
        if ((int)F == 2) Direc.x = -1f;
        if ((int)F == 3) Direc.x = 1f;



        //switch (F)
        //{
        //    case (Dir.Down):
        //        Direc.y = -1f;
        //        break;

        //    case (Dir.Left):
        //        Direc.x = -1f;
        //        break;

        //    case (Dir.Right):
        //        Direc.x = 1f;
        //        break;

        //    case (Dir.Up):
        //        Direc.y = 1f;
        //        break;

        //    default: print("Direction Error - LC"); break;

        //};

        Go.GetComponent<Bullet>().SetStats((int)WeaponStats.Damage, (Direc * WeaponStats.Speed), WeaponStats.Lifetime, itemUsed);

    }


    void OnDisable()
    {
        if (myRigid != null) myRigid.velocity = new Vector2(0, 0);
        if (myAnim != null) myAnim.SetFloat("SpeedX", 0); myAnim.SetFloat("SpeedY", 0);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy") TakeDamage(1);
        
    }

    public void TakeDamage(int damage)
    {
        if (dmgCD) return;
        if (health <= 0) return;
        StartCoroutine(DamageCooldown());
        health -= damage;
        UI.currentHealth = health;
        UI.ShowHearts();
        audioManager.Play("Player_Take_Damage_1");
        if (health <= 0)
        {
            audioManager.Play("Death_1");
            StartCoroutine(GameReset());
            enabled = false;
        }

        //Inv.addXP(Inv.equippedA, -3);
        //Inv.addXP(Inv.equippedB, -3);
    }

    IEnumerator GameReset()
    {
        if (DeathCanvas != null) DeathCanvas.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Main Menu");
        DoNotDes[] Gos = FindObjectsOfType<DoNotDes>();
        DoNotDes.Created = false;

        foreach (DoNotDes go in Gos) if (go.gameObject != gameObject) Destroy(go.gameObject);

        yield return new WaitForSeconds(0); Destroy(gameObject);
    }


    IEnumerator DamageCooldown()
    {
        dmgCD = !dmgCD;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        yield return new WaitForSeconds(0.5f);
        dmgCD = !dmgCD;
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }


    public void Heal(int value)
    {
        health = Mathf.Clamp(health + value, 0, UI.maxHealth);
        UI.currentHealth = health;
        audioManager.Play("Healing_1");
    }


     void SetRotater()
    {
        if (myRigid.velocity.x > 0.1)
        {
            render.flipX = false;
            aR.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (myRigid.velocity.x < -0.1)
        {
            render.flipX = true;
            aR.rotation = new Quaternion(0, 0, 180, 0);
        }
        if (myRigid.velocity.y > 0.1)
        {
            aR.rotation = new Quaternion(0, 0, 0, 0);
            if (aR.rotation != new Quaternion(0, 0, 90, 0)) aR.Rotate(Vector3.forward * 90);
        }
        if (myRigid.velocity.y < -0.1)
        {
            aR.rotation = new Quaternion(0, 0, 180, 0);
            if (aR.rotation != new Quaternion(0, 0, -90, 0))
                aR.Rotate(Vector3.forward * 90);
        } 

    }

}