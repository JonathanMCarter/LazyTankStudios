using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AssignTransformVariable : Ability
{
    public TransformVariable Target;
    public override void Use(object o)
    {
        Entity e = (Entity)o;
        Target.Value = e.mTransform;
        e.abilities.Remove(this);
    }
}
