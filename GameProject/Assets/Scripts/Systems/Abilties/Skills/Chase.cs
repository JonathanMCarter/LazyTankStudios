using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Chase : Ability
{
    public TransformVariable target;
    public FloatVariable speed;

    public override void Use(Entity owner)
    {
        if (target == null) return;
        if((target?.Value.position - owner.transform.position)?.magnitude > 0.5f)
        owner.transform.position += (target.Value.position - owner.transform.position).normalized * Time.deltaTime * speed.Value;
    }
}
