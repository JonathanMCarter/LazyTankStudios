using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Combat
    {
        // Don't know if there will be npc player on the Player side, therefore a List. Later it may be only the Player Combat System
        private List<AIEnemy> team1;
        // All enemies in that fight
        private List<AIEnemy> team2;
        //  State machine for the fight
        private FiniteStateMachine fsm;
        private EnemyTurn team1Turn;
        private EnemyTurn team2Turn;
        private IEnumerator combatLoop;
        private bool inCombat = false;
        private MonoBehaviour mb;

        public Combat(MonoBehaviour mb, List<AIEnemy> team1, List<AIEnemy> team2)
        {
            this.mb = mb;
            this.team1 = team1;
            this.team2 = team2;
            team1Turn = new EnemyTurn(mb, this, team1, team2);
            team2Turn = new EnemyTurn(mb, this, team2, team1);
            fsm = new FiniteStateMachine(mb, team1Turn);
        }

        public void StartCombat()
        {
            combatLoop = CombatLoop();
            inCombat = true;
            mb.StartCoroutine(combatLoop);
        }

        // Maybe later a Turn state
        public void NextTurn() => fsm.ChangeState(fsm._CurrentState == team1Turn ? team2Turn : team1Turn);

        private IEnumerator CombatLoop()
        {
            while (inCombat)
            {
                fsm.ExecuteCurrentState();
                yield return null;
            }
        }
    } 
}
