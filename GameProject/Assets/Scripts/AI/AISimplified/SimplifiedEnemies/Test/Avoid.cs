using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Avoid : Ability
    {
        public float avoidSpeed;

        public override void Interrupt() { }

        public override void Use()
        {
            Vector3 dest = (transform.position - Player.transform.position);
            transform.position += dest.magnitude > 8f ? Vector3.zero : dest.normalized * avoidSpeed * Time.deltaTime;
        }
    }
}
