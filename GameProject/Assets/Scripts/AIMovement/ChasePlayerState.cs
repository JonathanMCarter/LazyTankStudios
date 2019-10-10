using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class ChasePlayerState : State
    {
        private AIMovement ai;
        private Player player;

        public ChasePlayerState(MonoBehaviour owner, Player player) : base(owner)
        {
            this.ai = (AIMovement)owner;
            this.player = player;
        }

        public override void StateEnter()
        {
        }
        public override void Execute()
        {
            //Only for testing, should call ChasePlayer Method in aimovment
            ChasePlayer();
        }

        public override void StateExit()
        {
        }

        private void ChasePlayer()
        {
            if ((player.transform.position - ai.gameObject.transform.position).magnitude > 0.5f)
                ai.Move(player.transform.position);
        }
    }
}