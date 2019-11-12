using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Kamaitachi : Ability
    {
        public float AvoidTime;
        public float AvoidSpeed;
        public float ChaseTime;
        public float ChaseSpeed;


        private IEnumerator coroutine;
        private Avoid avoid;
        private Chase chase;
        
        private void Awake()
        {
            CastTime = AvoidTime + ChaseTime;
        }

        public override void Interrupt()
        {
            if (coroutine == null) return;
            StopCoroutine(coroutine);
        }

        public override void Use()
        {
            if(GetComponent<Avoid>() == null)
            {
                avoid = gameObject.AddComponent<Avoid>();
                avoid.avoidSpeed = AvoidSpeed;
                avoid.Start();
                chase = gameObject.AddComponent<Chase>();
                chase.chaseSpeed = ChaseSpeed;
                chase.Start();
            }
            StartCoroutine(coroutine = Coroutine());
        }

        private IEnumerator Coroutine()
        {
            float time = 0f;
            while(time < AvoidTime)
            {
                time += Time.deltaTime;
                avoid.Use();
                yield return null;
            }
            time = 0f;
            while (time < ChaseTime)
            {
                time += Time.deltaTime;
                chase.Use();
                yield return null;
            }
        }
    } 
}
