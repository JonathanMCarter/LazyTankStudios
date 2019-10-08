using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class EnemyTurn : State
    {
        // Enemies in the team
        private List<AIEnemy> ownTeam;
        private List<AIEnemy> enemyTeam;
        private Combat combat;

        public EnemyTurn(MonoBehaviour owner, Combat combat, List<AIEnemy>ownTeam, List<AIEnemy> enemyTeam) : base(owner)
        {
            this.ownTeam = ownTeam;
            this.enemyTeam = enemyTeam;
            this.combat = combat;
        }

        public override void StateEnter()
        {
            Debug.Log("Enemey Turn");
        }

        public override void Execute()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("Did something clever this Turn");
                ownTeam[0].Attack(enemyTeam[0]);
                combat.NextTurn();
            }
        }

        public override void StateExit()
        {
            Debug.Log("Eneme Turn over");
        }
    }
}
