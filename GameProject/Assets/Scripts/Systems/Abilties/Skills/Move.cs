using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Move : Ability
{
    public FloatVariable horizontal;
    public FloatVariable vertical;
    public FloatVariable speed;

    public override void Use(object o)
    {
        horizontal.Value = Input.GetAxisRaw("Horizontal");
        vertical.Value = Input.GetAxisRaw("Vertical");

        ((Entity)o).mTransform.position += new Vector3(horizontal.Value, vertical.Value).normalized * Time.deltaTime * speed.Value;
    }
}
