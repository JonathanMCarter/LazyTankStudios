using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public abstract class Ability : MonoBehaviour
    {
        /**
        *Edit by Andreas Kraemer
        *Last Edit: 11/11/19
        *
        *Set Animation Parameters
        */
        protected PlayerMovement Player;
        public float ExpiryTime = 0f;
        public Animation AnimationClip;
        protected Animator myAnimator;
        public abstract IEnumerator Use();
        public virtual void Awake()
        {
            Player = FindObjectOfType<PlayerMovement>();
            myAnimator=GetComponent<Animator>();
        }
    } 
}
