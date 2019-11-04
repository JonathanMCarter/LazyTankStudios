//using System.Collections;
//using UnityEngine;

//[CreateAssetMenu(fileName = "Jump", menuName ="Abilities/Jump")]
//public class Jump : ActionOverTime
//{
//    public PlayerVariable Player;
//    public float speed;
//    private Entity entity;

//    public override bool CanDoBoth()
//    {
//        return false;
//    }

//    public override IEnumerator Execute(Entity entity)
//    {
//        if (loop != null && !ISCOMPLETE) yield break;
//        this.entity = entity;
//        yield return loop = JumpLoop();
//    }

//    public IEnumerator JumpLoop()
//    {
//        Debug.Log(Player.Value.transform.position);
//        if (AnimationClip != null) AnimationClip.Play();
//        float progress = 0f;
//        Vector3 start = entity.transform.position;
//        Vector3 target = start + (Player.Value.transform.position - start).normalized;

//        while (progress <= 1.0f)
//        {
//            entity.transform.position = Vector3.Lerp(start, target, progress);
//            progress += Time.deltaTime / speed;
//            yield return null;
//        }
//    }
//}
