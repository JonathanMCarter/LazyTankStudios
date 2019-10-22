using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 
 * Made by Jonathan Carter
 * 20/10/19 - at a early hour in the morning
 * 
 * reason: cause it didn't exsist
 * 
 * 
 * 
 * 
 * 
 * LC Notes: Currently will not work, will keep scaling player damage up if it did work as intended. Need to move weapon damage onto projectile, and increase it on there. Would also allow a bool to be on that new bullet script to show if it can be affected by gates or not?
 * Can also use layers (player and enemy layers) to define if a gate is only for player or enemy or both
 */
public class PuzzleGateScript : MonoBehaviour
{
    public float ScaleSize;
    public enum Effect
    {
        Speed,
        DoubleDMG,
        IncreaseSize,
        Fire,
    };

    public Effect GateEffect;
    public PlayerMovement Player;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the object can pass through the gate (only the projectiles)
        if (collision.gameObject.tag == "Bullet")  //Will need to change identification of if can pass - As may need to use tags for another purpose later on (early warning to think about) comment added by LC
        {
            switch (GateEffect)
            {
                // Should make it so what passes though doubles in speed... (may run more than once though by accident)
                case Effect.Speed:
                    collision.GetComponent<Bullet>().IncreaseSpeed(); //added by LC
                    //collision.gameObject.GetComponent<Rigidbody2D>().velocity *= 2;
                    break;
                // Should alter the dmg to be doubled (this currently doesn't undo)
                case Effect.DoubleDMG:
                    collision.GetComponent<Bullet>().IncreaseDamage(2); //Added by LC
                    //Player.WeaponStats.Damage *= 2;
                    break;
                // Should double the scale of what passes through
                case Effect.IncreaseSize:
                    collision.transform.localScale *= ScaleSize;
                    break;
                // does nothing - no idea what to do with this one.....
                case Effect.Fire:
                    break;
                default:
                    break;
            }
        }
    }
}
