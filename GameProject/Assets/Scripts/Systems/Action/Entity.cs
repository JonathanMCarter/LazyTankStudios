using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Entity : MonoBehaviour
{
    [SerializeField] private List<Action> actions = new List<Action>();
    [SerializeField] private List<Action> updateActions = new List<Action>();
    public VoidEvent OnCollision;
    private void Start()
    {
        foreach (var action in actions)
        {
            Debug.Log($"Execute : {action}");
            StartCoroutine(action.Execute(this));
        }
    }

    private void Update()
    {
        foreach (var action in updateActions)
        {
            StartCoroutine(action.Execute(this));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision?.Raise();
    }
}