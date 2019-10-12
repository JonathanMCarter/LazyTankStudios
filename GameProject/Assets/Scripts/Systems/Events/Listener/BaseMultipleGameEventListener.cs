using UnityEngine;
using UnityEngine.Events;

public abstract class BaseMultipleGameEventListener<T, E, UER> : MonoBehaviour where E : BaseGameEvent<T> where UER : UnityEvent<T>
{
    protected abstract void OnEnable();
    protected abstract void OnDisable();
}

