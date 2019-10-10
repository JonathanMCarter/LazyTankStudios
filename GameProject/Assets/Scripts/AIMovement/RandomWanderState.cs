using System.Collections;
using UnityEngine;

namespace AI
{
    public class RandomWanderState : State
    {
        private AIMovement owner;
        private Vector2 rootPos;
        private int xRange;
        private int yRange;
        private bool IsReadyToMove;

        public RandomWanderState(AIMovement owner) : base(owner)
        {
            this.owner = owner;
            rootPos = owner.RootPos;
            xRange = owner.XRange;
            yRange = owner.YRange;
            IsReadyToMove = false;
        }

        public override void StateEnter()
        {
            owner.Move(GetDestination(), Idle);
        }

        public override void Execute()
        {
            if (IsReadyToMove)
            {
                IsReadyToMove = false;
                owner.Move(GetDestination(), Idle);
            }

        }

        public override void StateExit()
        {
        }

        public void Idle()
        {
            float idleTime = Random.Range(0.0f, owner.MaxIdleTime);
            owner.StartCoroutine(Idle(idleTime));
        }

        private IEnumerator Idle(float time)
        {
            yield return new WaitForSeconds(time);
            IsReadyToMove = true;
        }

        private Vector2 GetDestination() => new Vector2(Random.Range(rootPos.x - xRange / 2, rootPos.x + xRange / 2), Random.Range(rootPos.y - yRange / 2, rootPos.y + yRange / 2));
    }
}
