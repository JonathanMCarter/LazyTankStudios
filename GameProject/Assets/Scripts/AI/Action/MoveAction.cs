using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class MoveAction : Action
    {
        public Vector2 target;
        public float speed;

        public void Init(Vector2 target, float speed)
        {
            this.target = target;
            this.speed = speed;
            ActionDelegate = Move;
        }

        public void Move(MonoBehaviour mb, float deltaTime)
        {
            Vector3 dest = ((Vector3)target - mb.transform.position);
            mb.transform.position += dest.magnitude < .1f ? Vector3.zero : dest.normalized * deltaTime * speed;
        }
    }
}