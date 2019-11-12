using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
     /**
        *Edit by Andreas Kraemer
        *Last Edit: 11/11/19
        *
        *Set Animation Parameters
        */
    public class Chase : Ability
    {
        [SerializeField] private float chaseSpeed;
        private ActionOverTime aot;

        public override void Awake()
        {
            base.Awake();
            aot = new ActionOverTime();
            aot.ActionDelegate = ChaseExecute;
            aot.ExpiryTime = ExpiryTime;
        }

        public override IEnumerator Use()
        {
            if (AnimationClip != null) AnimationClip.Play();
            yield return aot.Use();
        }

        public void ChaseExecute()
        {
            Vector3 dest = (Player.transform.position - transform.position);
             //Andreas edit--
            //myAnimator.SetFloat("SpeedX",dest.x);
            //myAnimator.SetFloat("SpeedY",dest.y);
            //Andreas edit end--
            transform.position += dest.magnitude < 0.1f ? Vector3.zero : dest.normalized * Time.deltaTime * chaseSpeed;
        }
    } 
}