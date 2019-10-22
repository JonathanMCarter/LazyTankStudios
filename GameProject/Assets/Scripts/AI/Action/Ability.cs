using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Test
{
    public abstract class Ability : ScriptableObject
    {
        public Action Action { get; set; }

        public abstract IEnumerator Use(MonoBehaviour mb, float deltaTime);
    } 
}
