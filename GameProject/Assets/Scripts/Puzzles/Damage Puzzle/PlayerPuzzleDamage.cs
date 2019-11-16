
using UnityEngine;
public class PlayerPuzzleDamage : A
{
    float DamageTimer;
    public float MaxTime;
    bool Active;
    public int Damage;
    PlayerMovement Me;
    void Start()
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
            else DamageTimer += Time.deltaTime;
        }       
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            DamageTimer = MaxTime;
            Active = true;
        }          
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fire")) Active = false;

    }
}

