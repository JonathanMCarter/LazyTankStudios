using System.Collections;
using UnityEngine;

namespace Test
{
    [CreateAssetMenu]
    public class Chase : Ability
    {
        public Transform Target;
        public float ChaseTime;
        public float ChaseSpeed;

        private ActionOverTime aot;
        private MoveAction moveAction;

        public void Init()
        {
            moveAction = new MoveAction();
            aot = new ActionOverTime();
            aot.ExpiryTime = ChaseTime;
            aot.ActionDelegate = ChaseExecute;
        }

        public override IEnumerator Use(MonoBehaviour mb, float deltaTime)
        {
            yield return aot.Use(mb, deltaTime);
        }

        private void ChaseExecute(MonoBehaviour mb, float deltaTime)
        {
            moveAction.Init(Target.position, ChaseSpeed);
            moveAction.Move(mb, deltaTime);
        }
    } 
}
