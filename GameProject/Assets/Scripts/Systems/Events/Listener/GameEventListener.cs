using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour, IGameEventListener
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private UnityEvent response;

    private void OnEnable() => gameEvent.AddListener(this);
    private void OnDisable() => gameEvent.RemoveListener(this);

    public void OnEventRaised()
    {
        response?.Invoke();
    }
}
