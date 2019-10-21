using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActionSequence : ActionOverTime
{
    public List<Action> SubActions = new List<Action>();
    new public bool CanInterrupt => currentAction.CanInterrupt;

    private Queue<Action> subActions;
    private Action currentAction;

    public override IEnumerator Execute(Entity entity)
    {
        if (loop != null && !ISCOMPLETE) yield break;
        Debug.Log("Start Loop");
        loop = Loop(entity);
        yield return entity.StartCoroutine(loop);

    }

    private IEnumerator Loop(Entity entity)
    {
        subActions = new Queue<Action>(SubActions);

        while(subActions.Count != 0)
        {
            currentAction = subActions.Dequeue();

            while(!currentAction.IsComplete())
            {
                yield return currentAction.Execute(entity);
            }
        }
    }

    public override bool IsComplete()
    {
        return ISCOMPLETE && Reset();
    }

    public override bool CanDoBoth()
    {
        throw new System.NotImplementedException();
    }

    protected override bool Reset()
    {
        loop = null;
        return true;
    }
}