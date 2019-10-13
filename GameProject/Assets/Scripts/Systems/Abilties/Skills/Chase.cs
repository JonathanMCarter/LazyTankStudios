using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Chase : Ability
{
    public TransformVariable owner;
    public TransformVariable target;
    public FloatVariable speed;

    public override void Use(object o)
    {
        if ((owner.Value.position - target.Value.position).magnitude > 0.1f)
            owner.Value.position += (target.Value.position - owner.Value.position).normalized * Time.deltaTime * speed.Value;
    }
}
