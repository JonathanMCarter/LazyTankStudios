using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyProjectileMove : MonoBehaviour
{
    public float S;
    Rigidbody2D R;
    void Start()
    {
        R = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        R.velocity = transform.up * S;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {

        }
        
          Destroy(gameObject);
    }
}