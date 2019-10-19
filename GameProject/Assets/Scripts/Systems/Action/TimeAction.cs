using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TimeAction : ActionOverTime
{
    public override bool CanDoBoth()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Execute(Entity mb)
    {
        if (loop != null && !ISCOMPLETE) yield break;
        loop = Loop();
        yield return loop;
    }

    private IEnumerator Loop()
    {
        float progress = 0f;
        float time = 0f;

        while (progress < 1.0f)
        {
            time += Time.deltaTime;
            progress = time / ExpiryTime;
            yield return null;
        }
    }

    public override bool IsComplete()
    {
        return ISCOMPLETE && Reset();
    }

    protected override bool Reset()
    {
        loop = null;
        return true;
    }
}