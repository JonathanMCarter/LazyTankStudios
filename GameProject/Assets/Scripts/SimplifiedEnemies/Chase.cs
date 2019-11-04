using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public class Chase : Ability
    {
        [SerializeField] private float chaseSpeed;
        private ActionOverTime aot;

        private void Awake()
        {
            aot = new ActionOverTime();
            aot.ActionDelegate = ChaseExecute;
            aot.ExpiryTime = ExpiryTime;
        }

        public override IEnumerator Use()
        {
            yield return aot.Use();
        }

        public void ChaseExecute()
        {
            Vector3 dest = (Player.Value.transform.position - transform.position);
            transform.position += dest.magnitude < 0.1f ? Vector3.zero : dest.normalized * Time.deltaTime * chaseSpeed;
        }
    } 
}