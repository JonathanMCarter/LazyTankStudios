using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TGameEventListener<T, E, UER> : MonoBehaviour, IGameEventListener<T> where E : GameEvent<T> where UER : UnityEvent<T>
{
    [SerializeField] private E GameEvent;
    [SerializeField] private UER EventResponse;

    public void OnEventRaised(T data) => EventResponse?.Invoke(data);

    private void OnEnable() => GameEvent?.AddListener(this);
    private void OnDisable() => GameEvent?.RemoveListener(this);
}