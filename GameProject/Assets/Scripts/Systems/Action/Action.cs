using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public float ExpiryTime;
    public bool CanInterrupt;
    public Animation AnimationClip;
    public bool ISCOMPLETE { get; protected set; } = false;
    public abstract IEnumerator Execute(Entity mb);
    public virtual bool IsComplete()
    {
        return ISCOMPLETE && Reset();
    }
    public abstract bool CanDoBoth();
    protected virtual bool Reset()
    {
        return !(ISCOMPLETE = false);
    }
}

public abstract class ActionOverTime : Action
{
    protected IEnumerator loop;
    new public bool ISCOMPLETE => loop == null ? false : !loop.MoveNext();
    protected override bool Reset()
    {
        loop = null;
        return true;
    }
}