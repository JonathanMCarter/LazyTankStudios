using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEvent<T> : ScriptableObject
{
    protected List<IBaseGameEventListener<T>> listeners = new List<IBaseGameEventListener<T>>();

    public void Raise(T data)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(data);
        }
    }

    public void AddListener(IBaseGameEventListener<T> listener)
    {
        if (!listeners.Contains(listener)) listeners.Add(listener);
    }

    public void RemoveListener(IBaseGameEventListener<T> listener)
    {
        if (listeners.Contains(listener)) listeners.Remove(listener);
    }

}