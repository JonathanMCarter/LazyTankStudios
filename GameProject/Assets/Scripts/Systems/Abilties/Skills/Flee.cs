using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Flee : Ability
{
    public TransformVariable target;
    public FloatVariable speed;

    public override void Use(Entity owner)
    {
        if (target == null) return;
        if ((owner.transform.position - target.Value.position).magnitude < 10.0f)
            owner.transform.position += (owner.transform.position - target.Value.position).normalized * Time.deltaTime * speed.Value;
    }
}
