using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public abstract class Ability : MonoBehaviour
    {
        protected PlayerMovement Player;
        public float ExpiryTime = 0f;
        public Animation AnimationClip;
        public abstract IEnumerator Use();
        public virtual void Awake()
        {
            Player = FindObjectOfType<PlayerMovement>();
        }
    } 
}
