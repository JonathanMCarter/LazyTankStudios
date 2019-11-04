using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class Gyre : TestAIMovement
{
    [SerializeField] private float spinTime;
    [SerializeField] private float chargeTime;
    [SerializeField] private float chargeSpeed;
    private Spin spin;
    private Charge charge;
    
    void Start()
    {
        spin = ScriptableObject.CreateInstance<Spin>();
        spin.Init(spinTime);
        charge = ScriptableObject.CreateInstance<Charge>();
        charge.Init(player.Value.transform.position, chargeTime, chargeSpeed);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Player")
        {
            //currentAction = spin.Use(this, Time.deltaTime);
            charge.Target = (player.Value.transform.position - transform.position).normalized;
            currentAction = charge.Use(this, Time.deltaTime);
            StartCoroutine(currentAction);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        //StopCoroutine(currentAction);
        //base.OnTriggerExit2D(other);
    }
}
