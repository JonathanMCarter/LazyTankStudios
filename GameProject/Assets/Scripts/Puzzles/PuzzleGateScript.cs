using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 
 * Made by Jonathan Carter
 * 20/10/19 - at a early hour in the morning
 * 
 * reason: cause it didn't exsist
 */ 
public class PuzzleGateScript : MonoBehaviour
{
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
        if (collision.gameObject.tag == "CanPass")
        {
            switch (GateEffect)
            {
                // Should make it so what passes though doubles in speed... (may run more than once though by accident)
                case Effect.Speed:
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity *= 2;
                    break;
                // Should alter the dmg to be doubled (this currently doesn't undo)
                case Effect.DoubleDMG:
                    Player.WeaponStats.Damage *= 2;
                    break;
                // Should double the scale of what passes through
                case Effect.IncreaseSize:
                    collision.transform.localScale *= 2;
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
