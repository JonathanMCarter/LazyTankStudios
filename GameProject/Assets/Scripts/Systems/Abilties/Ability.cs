using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string Name;

    public abstract void Use();
}

public class Move : Ability
{
    public override void Use(Entity entity)
    {
        throw new System.NotImplementedException();
    }
}