using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public class Wait : Ability
    {
        private ActionOverTime aot;

        public override void Awake()
        {
            base.Awake();
            aot = new ActionOverTime();
            aot.ActionDelegate = ()=> { };
            aot.ExpiryTime = ExpiryTime;
        }

        public override IEnumerator Use()
        {
            if (AnimationClip != null) AnimationClip.Play();
            yield return aot.Use();
        }
    }
}