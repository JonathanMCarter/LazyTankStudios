using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class ChaseAction : ActionOverTime
    {
        private Transform target;
        private MoveAction moveAction;

        public void Init(Transform target, float chaseTime, float chaseSpeed)
        {
            this.target = target;
            ExpiryTime = chaseTime;
            moveAction = ScriptableObject.CreateInstance<MoveAction>();
            moveAction.Init(target.position, chaseSpeed);
            ActionDelegate = Chase;
        }

        private void Chase(MonoBehaviour mb, float deltaTime)
        {
            moveAction.target = target.position;
            moveAction.Move(mb, deltaTime);
        }


    } 
}
