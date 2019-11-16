using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewAIMove : MonoBehaviour
{
    public float MoveSpeed;
    public float RotateSpeed;
    float WaitTime;
    public float Direction;
    float TurnTime;
    public Transform me;
    Transform Player;
    public Rigidbody2D MyRigid;
    public Vector2 Paramiers;
    public Vector2 WaitVarables;
    bool SeenPlayer;
    bool Turn;
    bool TurnCount;
    bool ToggleDirection;
    public SpriteRenderer[] Hearts;
    private bool hit;
    public int Health;
    public float DamageCD=0.3f;

    private PlayerMovement player;
    private Inventory playerInventory;
    
    [HideInInspector]


    void Start()
    {
        TurnCount = true;
        MyRigid = GetComponent<Rigidbody2D>();
        hit = false;
        playerInventory = FindObjectOfType<Inventory>();
        Health=Hearts.Length;
    }
    void Update()
    {

        me.transform.position = transform.position;
        if (SeenPlayer)
            RunToPlayer();
        else
            RandomWander();

        
    }
    private void FixedUpdate()
    {

        if (ToggleDirection || SeenPlayer)
        {
            ToggleDirection = true;
            if (!SeenPlayer)

                MyRigid.angularVelocity = 0;
            MyRigid.velocity = transform.up * (MoveSpeed * Time.deltaTime);
        }
        else
            MyRigid.velocity = transform.up * (-MoveSpeed * Time.deltaTime);
        if (Turn)
        {
            if (Direction < 50)
                transform.Rotate(0, 0, RotateSpeed * Time.deltaTime);
            else if (Direction >= 50)
                transform.Rotate(0, 0, -RotateSpeed * Time.deltaTime);
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
        Transform PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector2 Direction = new Vector2(PlayerPosition.position.x - transform.position.x, PlayerPosition.position.y - transform.position.y);
        transform.up = Direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject.transform;
                SeenPlayer = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
                SeenPlayer = false;
                ToggleDirection = false;
                Player = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ToggleDirection)
            ToggleDirection = false;
        else
            ToggleDirection = true;

            
            if (collision.gameObject.tag == "Bullet" && !hit)
            {
                hit = true;
                //Toby: get bullet damage instead of always 1
                Bullet b = collision.gameObject.GetComponent<Bullet>();
                int damage = b.Damage;
                playerInventory.addXP(b.SourceItem, 1);

                Destroy(collision.gameObject);

                // Debug.Log("********** Enemy Should Be Taking Damage Now...");

                if (Health > 0) Hearts[Health - 1].gameObject.SetActive(false); //if statement added by LC to avoid potential errors
                Health -= damage;
                if (Health <= 0)
                    this.gameObject.SetActive(false);
                StartCoroutine(DamageCooldown());
            }
            if (collision.gameObject.tag == "Sword" && !hit)
            {
                hit = true;
                Bullet b = collision.gameObject.GetComponent<Bullet>();
                int damage = b.Damage;
                playerInventory.addXP(b.SourceItem, 1);
                // Debug.Log("********** Enemy Should Be Taking Damage Now...");

                if (Health > 0) Hearts[Health - 1].gameObject.SetActive(false);
                Health -= damage;
                StartCoroutine(DamageCooldown());
                if (Health <= 0)
                    this.gameObject.SetActive(false);
                
            }
       

    }

     IEnumerator DamageCooldown() //temp add by LC
    {
        
        yield return new WaitForSeconds(DamageCD);
        hit=false;
    }


    
}
