using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// Graham's FirePuzzle script, cannot test until player is fixed in my scene as causing damage causes a crash 
//(missing game objects in playerMovement script) can be fixed in the inspector however i do not know what goes where as i know nothing

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
        //if(collision.gameObject.tag == "Fire")
        //{                      
        //        gameObject.GetComponent<PlayerMovement>().TakeDamage(Damage);           
        //}             
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Active = false;
        }
        //if (other.gameObject.tag == "Fire")
        //    Active = false;
    }
}

