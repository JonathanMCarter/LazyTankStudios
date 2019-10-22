using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerPuzzleDamage : MonoBehaviour
{
    public float DamageTimer;
    public float MaxTime;
    public bool Active;
    public int Damage;
    public float time;
    PlayerMovement Me;
    private void Start()
    {
        Me = GetComponent<PlayerMovement>();
    }
    void Update()
    {
       if (Active)
        {
            if (DamageTimer >= MaxTime)
            {                              
                Debug.Log("TakenDamage");
                DamageTimer = 0;
                Me.TakeDamage(Damage);
            }
            else
            {
                time = Time.deltaTime;
                DamageTimer += time;
            }
        }       
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            Me.TakeDamage(Damage);
            Active = true;
        }          
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Active = false;
        }
    }
}

