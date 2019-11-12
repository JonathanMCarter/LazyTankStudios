using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Wait : Ability
    {
        private IEnumerator coroutine;

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
            yield return new WaitForSeconds(CastTime);
        }
    }
}
