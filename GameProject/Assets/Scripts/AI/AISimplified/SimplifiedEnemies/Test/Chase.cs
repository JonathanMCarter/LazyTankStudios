using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Chase : Ability
    {
        public float chaseSpeed;
        public GameObject Target;

        public override void Start()
        {
            base.Start();
            Target = Player.gameObject;
        }

        public override void Interrupt(){}

        public override void Use()
        {
            Vector3 dest = (Target.transform.position - transform.position);
            transform.position += dest.magnitude < 1f ? Vector3.zero : dest.normalized * chaseSpeed * Time.deltaTime;
        }
    }
}
