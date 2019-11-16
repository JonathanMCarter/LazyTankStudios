using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerPuzzleDamage : MonoBehaviour
{
    float DamageTimer;
    public float MaxTime;
    bool Active;
    public int Damage;
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
                DamageTimer = 0;
                Me.TakeDamage(Damage);
            }
            else
            {
                DamageTimer += Time.deltaTime;
            }
        }       
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            DamageTimer = MaxTime;
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

