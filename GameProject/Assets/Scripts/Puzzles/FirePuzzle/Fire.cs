using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public int Damage;
    public float ActiveSeconds;
    public float WaitSeconds;
    private float Timer;
    public float damageTime;
    private float damageTimer;

    private bool Active;
    private bool DamageTrigger;


    private BoxCollider2D MyCol;
    public Sprite ActiveSprite;
    public Sprite WaitSprite;
    private SpriteRenderer MyRend;
    private PlayerMovement MyPlay;

    private void Start()
    {
        Timer = 0f;
        Active = false;
        MyCol = GetComponent<BoxCollider2D>();
        MyRend = GetComponent<SpriteRenderer>();
        MyPlay = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if (!Active)
        {
            if(Timer >= ActiveSeconds)
            {
                Active = true;
                Timer = 0f;
                MyCol.enabled = true;
                MyRend.sprite = ActiveSprite;
            }
        }
        else
        {
            if (Timer >= WaitSeconds)
            {
                Active = false;
                Timer = 0f;
                MyCol.enabled = false;
                MyRend.sprite = WaitSprite;
            }
        }
        if(DamageTrigger)           
        {
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageTime)
            {
                MyPlay.TakeDamage(Damage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            DamageTrigger = true;
            MyPlay.TakeDamage(Damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamageTrigger = false;
        }
    }
}
