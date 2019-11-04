using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class Slime : TestAIMovement
{
    private Chase chase;

    public void Start()
    {
        chase = ScriptableObject.CreateInstance<Chase>();
        chase.Init(player.Value.transform, int.MaxValue, MovementSpeed);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Player")
        {
            currentAction = chase.Use(this, Time.deltaTime);
            StartCoroutine(currentAction);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        StopCoroutine(currentAction);
        base.OnTriggerExit2D(other);
    }
}
