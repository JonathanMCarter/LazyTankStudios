using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public class Spin : Ability
    {
        private ActionOverTime aot;

        private void Awake()
        {
            aot = new ActionOverTime();
            aot.ActionDelegate = ()=>{ };
            aot.ExpiryTime = ExpiryTime;
        }

        public override IEnumerator Use()
        {
            yield return aot.Use();
        }
    }
}