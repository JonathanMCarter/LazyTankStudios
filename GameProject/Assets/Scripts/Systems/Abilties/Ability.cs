using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public bool IsComplete { get; private set; } = true;
    
    public abstract void Use(object o);
}

public abstract class AbilityOverTime : Ability
{
    public new bool IsComplete { get; private set; } = false;
    public float ExpiryTime;
    public override void Use(object o)
    {
        Entity e = ((Entity)o);
        e.StartCoroutine(ExpiryTimer());
    }

    private IEnumerator ExpiryTimer()
    {
        yield return new WaitForSeconds(ExpiryTime);
        IsComplete = true;
    }
}

public class AbilitySequence : Ability
{
    public Queue<Ability> abilities;
    public override void Use(object o)
    {
    }

    public bool Interrupt()
    {
        return true;
    }
    private IEnumerator StartSequence()
    {
        while(abilities.Count != 0)
        {
            Ability currentAbility = abilities.Dequeue();

            while(!currentAbility.IsComplete)
            {
                currentAbility.Use(null);
                yield return null;
            }
            yield return null;
        }
    }
}