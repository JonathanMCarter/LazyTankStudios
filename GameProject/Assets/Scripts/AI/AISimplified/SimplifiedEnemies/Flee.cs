using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public class Flee : Ability
    {
        [SerializeField] private float fleeSpeed;
        private ActionOverTime aot;

        public override void Awake()
        {
            base.Awake();
            aot = new ActionOverTime();
            aot.ActionDelegate = FleeExecute;
            aot.ExpiryTime = ExpiryTime;
        }

        public override IEnumerator Use()
        {
            AnimationClip.Play();

            yield return aot.Use();
        }

        public void FleeExecute()
        {
            Vector3 dest = -(Player.transform.position - transform.position);
            transform.position += dest.magnitude > 5f ? Vector3.zero : dest.normalized * Time.deltaTime * fleeSpeed;
        }
    }
}