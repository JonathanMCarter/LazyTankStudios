using UnityEngine;

namespace AI
{
    public class RandomWanderState : State
    {
        private AIMovement owner;
        private Vector2 rootPos;
        private int xRange;
        private int yRange;

        public RandomWanderState(AIMovement owner) : base(owner)
        {
            this.owner = owner;
        }

        public override void StateEnter()
        {
            rootPos = owner.RootPos;
            xRange = owner.XRange;
            yRange = owner.YRange;
        }

        public override void Execute()
        {
            if (!owner.IsMoving && owner.IsIdle)
                owner.MoveAccess(GetDestination());
        }

        public override void StateExit()
        {
        }

        private Vector2 GetDestination()
        {
            Vector2 v = new Vector2(rootPos.x + Random.Range(-xRange / 2, xRange / 2 + 1), rootPos.y + Random.Range(-yRange / 2, yRange / 2 + 1));
            Debug.Log(v);
            return v;
        }
    } 
}
