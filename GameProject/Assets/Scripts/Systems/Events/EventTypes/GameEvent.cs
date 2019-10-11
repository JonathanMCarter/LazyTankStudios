using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Game Event", menuName ="Events/Game Event")]
public class GameEvent : ScriptableObject
{
    private List<IGameEventListener> listeners = new List<IGameEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void AddListener(IGameEventListener listener)
    {
        if (!listeners.Contains(listener)) listeners.Add(listener);
    }

    public void RemoveListener(IGameEventListener listener)
    {
        if (listeners.Contains(listener)) listeners.Remove(listener);
    }
}
public abstract class GameEvent<T> : ScriptableObject
{
    private List<IGameEventListener<T>> listeners = new List<IGameEventListener<T>>();

    public void Raise(T data)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(data);
        }
    }

    public void AddListener(IGameEventListener<T> listener)
    {
        if (!listeners.Contains(listener)) listeners.Add(listener);
    }

    public void RemoveListener(IGameEventListener<T> listener)
    {
        if (listeners.Contains(listener)) listeners.Remove(listener);
    }

}
public interface IGameEventListener<T>
{
    void OnEventRaised(T data);
}