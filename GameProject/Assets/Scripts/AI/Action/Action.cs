using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Action : ScriptableObject
    {
        public ActionDelegate ActionDelegate { get; set; }
        public float ExpiryTime = 0f;
        public float elapsedTime;


        public virtual IEnumerator Use(MonoBehaviour mb, float deltaTime)
        {
            ActionDelegate(mb, deltaTime);
            yield return null;
        }
    }

    public delegate void ActionDelegate(MonoBehaviour mb, float deltatime);
}