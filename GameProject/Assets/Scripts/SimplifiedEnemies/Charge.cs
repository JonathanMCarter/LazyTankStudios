using System.Collections;
using UnityEngine;

namespace Final
{
    public class Charge : Ability
    {
        [SerializeField] private float chargeSpeed;
        [SerializeField] private float chargeDistance;
        [SerializeField] private float channelTime;
        private ActionOverTime aot;
        private Vector2 dir;


        private void Awake()
        {
            aot = new ActionOverTime();
            aot.ActionDelegate = ChargeExecute;
            ExpiryTime = (chargeDistance / chargeSpeed) + channelTime;
            aot.ExpiryTime = (chargeDistance / chargeSpeed);
        }

        public override IEnumerator Use()
        {
            Debug.Log("Start");
            if(AnimationClip != null) AnimationClip.Play();
            dir = (Player.Value.transform.position - transform.position).normalized;
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