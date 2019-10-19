using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private List<Action> actions = new List<Action>();
    [SerializeField] private List<Action> updateActions = new List<Action>();

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
            action.Execute(this);
        }
    }
}