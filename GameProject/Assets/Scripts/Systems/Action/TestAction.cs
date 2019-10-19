using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class TestAction : Action
{
    public override bool CanDoBoth()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Execute(Entity mb)
    {
        Debug.Log($"Executed {name}");
        ISCOMPLETE = true;
        yield return null;
    }

    public override bool IsComplete()
    {
        return ISCOMPLETE && Reset();
    }

    protected override bool Reset()
    {
        return !(ISCOMPLETE = false);
    }
}