using System.Collections;
using UnityEngine;
public class NewAIMove : A
{
    public float MoveSpeed,RotateSpeed,Direction;

    float WaitTime,TurnTime;

    public bool boss;
    public Transform me;
    Transform Player;
    Rigidbody2D MyRigid;
    public Vector2 Paramiers,WaitVarables;
    public Vector3 offset;
    bool SeenPlayer,Turn,TurnCount,ToggleDirection, hit;

    public SpriteRenderer[] Hearts;
    public int Health;
    public float DamageCD=0.3f;

    PlayerMovement player;
    //Inventory playerInventory;
    Transform PlayerPos;

    //Gabriel added it
    static int currBoss = 0;
    //Gabriel end
    //Andreas
    SoundPlayer sp;


    void Start()
    {
        TurnCount = true;
        MyRigid = GetComponent<Rigidbody2D>();
        //playerInventory = FindObjectOfType<Inventory>();
        Health=Hearts.Length;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        sp=FindObjectOfType<SoundPlayer>();
    }
    void Update()
    {

        me.transform.position = transform.position + offset;
        if (SeenPlayer) RunToPlayer();
           
        else  RandomWander();

        
    }
    void FixedUpdate()
    {

        if (ToggleDirection || SeenPlayer)
        {
            ToggleDirection = true;
            if (!SeenPlayer)   MyRigid.angularVelocity = 0;

            MyRigid.velocity = transform.up * MoveSpeed;//shouldnt use time.deltatime in fixed update as time between frames is not same as time between fixed update updates

        }
        else  MyRigid.velocity = transform.up * -MoveSpeed;
           
        if (Turn)
        {
            if (Direction < 50) transform.Rotate(0, 0, RotateSpeed);
            else if (Direction >= 50)  transform.Rotate(0, 0, -RotateSpeed);
              
        }
        
    }
    void RandomWander()
    {
        if (TurnCount)
        {
            TurnTime -= Time.deltaTime;
            Direction = Random.Range(1, 100);
            WaitTime = Random.Range(WaitVarables.x, WaitVarables.y);
            Turn = false;
            if (TurnTime <= 0)
            {
                TurnCount = false;
                Turn = true;
            }
        }
        else
        {
            TurnTime = Random.Range(Paramiers.x, Paramiers.y);
            WaitTime -= Time.deltaTime;
            if (WaitTime <= 0)
            {
                TurnCount = true;
            }
        }
    }
    void RunToPlayer()
    {
       
        Vector2 Direction = new Vector2(PlayerPos.position.x - transform.position.x, PlayerPos.position.y - transform.position.y);
        transform.up = Direction;
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject.transform;
                SeenPlayer = true;
        }
        
    }
     void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
                SeenPlayer = false;
                ToggleDirection = false;
                Player = null;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ToggleDirection)
            ToggleDirection = false;
        else
            ToggleDirection = true;
        if (collision.gameObject.tag == "Bullet")// && !hit)
            {
                //hit = true;
                //Toby: get bullet damage instead of always 1
                Bullet b = collision.gameObject.GetComponent<Bullet>();
                int damage = b.Damage;
               // playerInventory.addXP(b.SourceItem, 1);

                Destroy(collision.gameObject);

                // Debug.Log("********** Enemy Should Be Taking Damage Now...");

                if (Health > 0) Hearts[Health - 1].gameObject.SetActive(false); //if statement added by LC to avoid potential errors
                Health -= damage;
                  
                sp.Play("Take_Damage_3");
                //StartCoroutine(DamageCooldown());
            }
        if (Health <= 0)
        {
            if (boss)
            {
                Quest.boss[currBoss] = true;
                currBoss++;
            }
            gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Sword")// && !hit)
            {
                //hit = true;
                Bullet b = collision.gameObject.GetComponent<Bullet>();
                int damage = b.Damage;
              //  playerInventory.addXP(b.SourceItem, 1);
                // Debug.Log("********** Enemy Should Be Taking Damage Now...");

                if (Health > 0) Hearts[Health - 1].gameObject.SetActive(false);
                Health -= damage;
                 sp.Play("Take_Damage_3");
                //StartCoroutine(DamageCooldown());
                
                
            }
            if (Health <= 0) 
            {
                sp.Play("Death_3");
                gameObject.SetActive(false);
            }
    }
    // IEnumerator DamageCooldown() //temp add by LC
    //{
    //    yield return new WaitForSeconds(DamageCD);
    //    hit=false;
    //}
}
