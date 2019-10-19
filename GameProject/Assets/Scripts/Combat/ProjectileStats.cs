using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile")]
public class ProjectileStats : ScriptableObject
{
    public float Speed;
    public float Lifetime;
    public float Size;
    public float Damage;
}
