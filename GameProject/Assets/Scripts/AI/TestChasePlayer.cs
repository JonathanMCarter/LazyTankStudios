using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class TestChasePlayerState : State
    {
        private TestAIMovement ai;
        private List<Action> actions;
        private Action currentAction;
        private IEnumerator currentActionLoop;

        public TestChasePlayerState(MonoBehaviour owner, GameObject player) : base(owner)
        {
            this.ai = (TestAIMovement)owner;
            actions = ai.Actions;
        }

        public override void StateEnter()
        {
        }
        public override void Execute()
        {
            //Only for testing, should call ChasePlayer Method in aimovment
            //ai.ChasePlayer();
            if(currentAction != null)Debug.Log(currentAction.IsComplete());
            if (currentActionLoop != null && !currentAction.IsComplete()) return;
            Debug.Log("Start");
            currentAction = actions[Random.Range(0, actions.Count)];

            currentActionLoop = currentAction.Execute(ai.GetComponent<Entity>());
            ai.StartCoroutine(currentActionLoop);
        }

        public override void StateExit()
        {

                ai.StopAllCoroutines();
        }
    }
}