//using System.Collections;
//using UnityEngine;

//[CreateAssetMenu(fileName = "Wait", menuName = "Abilities/Wait")]
//public class Wait : ActionOverTime
//{
//    public override bool CanDoBoth()
//    {
//        return false;
//    }

//    public override IEnumerator Execute(Entity mb)
//    {
//        if (loop != null && !ISCOMPLETE) yield break;
//        yield return loop = WaitLoop();
//    }

//    public IEnumerator WaitLoop()
//    {
//        if (AnimationClip != null) AnimationClip?.Play();
//        yield return new WaitForSeconds(ExpiryTime);
//        Debug.Log($"Waited {ExpiryTime} seconds");
//    }

//    public override bool IsComplete()
//    {
//        return ISCOMPLETE && Reset();
//    }

//    protected override bool Reset()
//    {
//        loop = null;
//        return true;
//    }
//}
