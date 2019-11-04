using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : Ability
{
    public Vector2 Target;
    public float ChargeTime;
    public float ChargeSpeed;

    private ActionOverTime aot;

    public void Init(Vector2 target, float chargeTime, float chargeSpeed)
    {
        Target = target;
        ChargeSpeed = chargeSpeed;
        aot = new ActionOverTime();
        aot.ExpiryTime = chargeTime;
        aot.ActionDelegate = ChargeExecute;
    }

    public override IEnumerator Use(MonoBehaviour mb, float deltaTime)
    {
        yield return aot.Use(mb, deltaTime);
    }

    private void ChargeExecute(MonoBehaviour mb, float deltaTime)
    {
        Debug.Log("Charge");
        mb.transform.position += (Vector3)Target * ChargeSpeed * deltaTime;
    }
}
