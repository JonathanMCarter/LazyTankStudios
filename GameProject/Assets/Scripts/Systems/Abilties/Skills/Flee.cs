using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Flee : Ability
{
    public TransformVariable owner;
    public TransformVariable target;
    public FloatVariable speed;

    public override void Use(object o)
    {
        if ((owner.Value.position - target.Value.position).magnitude < 10.0f)
            owner.Value.position += (owner.Value.position - target.Value.position).normalized * Time.deltaTime * speed.Value;
    }
}
