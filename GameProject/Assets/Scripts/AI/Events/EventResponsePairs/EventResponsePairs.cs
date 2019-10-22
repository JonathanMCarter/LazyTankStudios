using UnityEngine.Events;

[System.Serializable]
public class EventResponsePairs<T, E, UER> : IBaseGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
{
    public E gameEvent;
    public UER eventResponse;

    public void OnEventRaised(T data) => eventResponse?.Invoke(data);
}