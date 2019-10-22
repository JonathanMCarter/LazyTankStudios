using System.Collections;
using UnityEngine;

namespace Test
{
    public class ActionOverTime : Action
    {
        public override IEnumerator Use(MonoBehaviour mb, float deltaTime)
        {
            float progress = 0f;
            elapsedTime = 0f;
            while(progress <= 1f)
            {
                Debug.Log(elapsedTime);
                elapsedTime += deltaTime;
                progress += deltaTime / ExpiryTime;
                ActionDelegate(mb, deltaTime);
                yield return null;
            }
            elapsedTime = 0f;
        }
    }
}