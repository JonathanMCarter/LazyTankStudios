﻿using UnityEngine;

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
            rootPos = owner.RootPos;
            xRange = owner.XRange;
            yRange = owner.YRange;
        }

        public override void StateEnter()
        {
            owner.RandomWander(GetDestination());
        }

        public override void Execute()
        {
            if (owner.IsReadyToMove)
                owner.RandomWander(GetDestination());

        }

        public override void StateExit()
        {
        }

        private Vector2 GetDestination() => new Vector2(Random.Range(rootPos.x - xRange / 2, rootPos.y + yRange / 2), Random.Range(rootPos.y - yRange / 2, rootPos.y + yRange / 2));
    } 
}
