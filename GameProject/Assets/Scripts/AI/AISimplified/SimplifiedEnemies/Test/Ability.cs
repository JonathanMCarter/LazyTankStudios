using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public abstract class Ability : MonoBehaviour
    {
        protected PlayerMovement Player;
        protected Animator myAnimator;
        protected float time;
        public float CastTime;

        public virtual void Start()
        {
            Player = FindObjectOfType<PlayerMovement>();
            myAnimator = GetComponent<Animator>();
        }
        public abstract void Use();

        public abstract void Interrupt();
    }
}
