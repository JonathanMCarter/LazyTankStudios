using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour, IBaseGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
{
    [SerializeField] private E GameEvent;
    [SerializeField] protected UER EventResponse;

    public virtual void OnEventRaised(T data) => EventResponse?.Invoke(data);

    private void OnEnable() => GameEvent?.AddListener(this);
    private void OnDisable() => GameEvent?.RemoveListener(this);
}