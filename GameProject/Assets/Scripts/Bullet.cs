using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * Created by: Lewis Cleminson
 * Last Edit: 21.10.19
 * 
 * Didnt want to create this script. Currently storing bullet speed and damage information
 * 
 * 
 * 
 * */


public class Bullet : MonoBehaviour
{
    public int Damage;
    public Vector2 Speed;
    public float Lifespan;
    public void SetStats(int damage, Vector2 speed, float lifespan)
    {
        Damage = damage;
        Speed = speed;
        Lifespan = lifespan;

        GetComponent<Rigidbody2D>().AddForce(Speed, ForceMode2D.Impulse);
        StartCoroutine(Destructor());
    }

    IEnumerator Destructor()
    {
        yield return new WaitForSeconds(Lifespan);
        //print("Destroyed Bullet with " + Speed + " Speed and " + Damage + " Damage" + ">>> UNCOMMENT ME IF YOU LIKE");
        Destroy(this.gameObject);
    }

    public void IncreaseSpeed()
    {
        GetComponent<Rigidbody2D>().AddForce(Speed, ForceMode2D.Impulse);
        Speed = Speed * 2;
    }

    public void IncreaseDamage(int damageChange)
    {
        Damage = Damage * 2;
    }
}
