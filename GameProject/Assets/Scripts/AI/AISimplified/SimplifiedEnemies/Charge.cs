using System.Collections;
using UnityEngine;

namespace Final
{
     /**
        *Edit by Andreas Kraemer
        *Last Edit: 11/11/19
        *
        *Set Animation Parameters
        */
    public class Charge : Ability
    {
        [SerializeField] private float chargeSpeed;
        [SerializeField] private float chargeDistance;
        [SerializeField] private float channelTime;
        private ActionOverTime aot;
        private Vector2 dir;


        public override void Awake()
        {
            base.Awake();
            aot = new ActionOverTime();
            aot.ActionDelegate = ChargeExecute;
            ExpiryTime = (chargeDistance / chargeSpeed) + channelTime;
            aot.ExpiryTime = (chargeDistance / chargeSpeed);
        }

        public override IEnumerator Use()
        {
            Debug.Log("Start");
            if(AnimationClip != null) AnimationClip.Play();
            dir = (Player.transform.position - transform.position).normalized;
            //Andreas edit--
            myAnimator.SetFloat("SpeedX",dir.x);
            myAnimator.SetFloat("SpeedY",dir.y);
            //Andreas edit end--
            yield return Coroutine();
        }

        public IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(channelTime);
            yield return aot.Use();
            Debug.Log("end");
        }

        public void ChargeExecute()
        {
            transform.position += (Vector3)dir * Time.deltaTime * chargeSpeed;
        }
    }

}