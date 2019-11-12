using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Jump : Ability
    {
        public float JumpLength;
        public float JumpSpeed;
        private IEnumerator coroutine;

        private void Awake()
        {
            CastTime = JumpLength / JumpSpeed;
        }

        public override void Interrupt()
        {
            if (coroutine == null) return;
            StopCoroutine(coroutine);
        }

        public override void Use()
        {
            StartCoroutine(coroutine = Coroutine());
        }

        private IEnumerator Coroutine()
        {
            Vector3 dir = (Player.transform.position - transform.position).normalized;
            Vector3 start = transform.position;

            while (transform.position != start + dir * JumpLength)
            {
                transform.position += dir * Time.deltaTime * JumpSpeed;
                yield return null;
            }
        }
    } 
}
