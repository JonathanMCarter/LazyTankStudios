using UnityEngine;



/*
 * Created by: Lewis Cleminson
 * Last Edit: 21.10.19
 * 
 * Didnt want to create this script. Currently storing bullet speed and damage information
 * 
 * Also edited by: Toby Wishart
 * Last edit: 11/10/19
 * Reason: Store source item and allow for unlimited lifespan for sword
 * 
 * */


public class Bullet : A
{
    public int Damage;
    public Vector2 Speed;
    public float Lifespan;
    //The ID of the item used to spawn projectile set to -1 or any other invalid ID for no source
    public int SourceItem = -1;

    public void SetStats(int damage, Vector2 speed, float lifespan, int sourceItem)
    {
        Damage = damage;
        Speed = speed;
        Lifespan = lifespan;
        SourceItem = sourceItem;

        GetComponent<Rigidbody2D>().AddForce(Speed, ForceMode2D.Impulse);
        //Allow for unlimited lifespan
        if (Lifespan >= 0) Destroy(gameObject, Lifespan);
    }



    public void IncreaseSpeed()
    {
        GetComponent<Rigidbody2D>().AddForce(Speed, ForceMode2D.Impulse);
        Speed = Speed * 2;
    }

    public void IncreaseDamage(int damageChange)
    {
        Damage = Damage * damageChange;
    }
}
