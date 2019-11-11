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
            if(AnimationClip!=null)AnimationClip.Play();

            yield return aot.Use();
        }

        public void FleeExecute()
        {
            Vector3 dest = -(Player.transform.position - transform.position);
             //Andreas edit--
            myAnimator.SetFloat("SpeedX",dest.x);
            myAnimator.SetFloat("SpeedY",dest.y);
            //Andreas edit end--
            transform.position += dest.magnitude > 5f ? Vector3.zero : dest.normalized * Time.deltaTime * fleeSpeed;
        }
    }
}