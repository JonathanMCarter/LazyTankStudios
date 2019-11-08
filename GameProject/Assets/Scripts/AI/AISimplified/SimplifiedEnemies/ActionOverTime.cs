using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public class ActionOverTime : Action
    {
        public float ExpiryTime = 0f;

        public override IEnumerator Use()
        {
            float progress = 0f;
            while (progress <= 1f)
            {
                progress += Time.deltaTime / ExpiryTime;
                ActionDelegate();
                yield return null;
            }
        }
    }
}
