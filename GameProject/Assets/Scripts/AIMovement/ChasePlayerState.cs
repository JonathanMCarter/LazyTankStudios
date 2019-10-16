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
            //Debug.Log(player.transform.position);
            //ai.Move(player.transform.position);
            if((player.transform.position - ai.transform.position).magnitude > .5f)
            ai.transform.position += (player.transform.position - ai.transform.position).normalized * Time.deltaTime * ai.MovementSpeed;
        }
    }
}