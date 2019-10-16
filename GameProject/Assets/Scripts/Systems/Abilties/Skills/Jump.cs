using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Jump : Ability
{
    public Vector2 dir;
    public FloatVariable strength;
    
    public override void Use(Entity o)
    {
        //o.transform.position +=  ;
    }
}
