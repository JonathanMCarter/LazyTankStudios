using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour, IBaseGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
{
    [SerializeField] private E gameEvent;
    [SerializeField] protected UER eventResponse;
    private void OnEnable() => gameEvent?.AddListener(this);
    private void OnDisable() => gameEvent?.RemoveListener(this);
    public virtual void OnEventRaised(T data) => eventResponse?.Invoke(data);
}