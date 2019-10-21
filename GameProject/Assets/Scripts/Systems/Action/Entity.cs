using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Entity : MonoBehaviour
{
    public VoidEvent OnCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision?.Raise();
    }
}