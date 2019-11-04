using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Ability : ScriptableObject
{
    public Action Action { get; set; }
    public Animation AnimationClip;
    public abstract IEnumerator Use(MonoBehaviour mb, float deltaTime);
}

