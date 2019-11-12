using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Torpid : Ability
    {
        public Sprite AttackSprite;
        public Sprite SleepSprite;
        public float SleepTime;
        public float ChaseSpeed;

        private IEnumerator coroutine;
        private SpriteRenderer renderer;

        private void Awake()
        {
            CastTime += SleepTime;
        }

        public override void Interrupt()
        {
            if (coroutine == null) return;
            renderer.sprite = AttackSprite;
            StopCoroutine(coroutine);
        }

        public override void Use()
        {
            renderer = GetComponent<SpriteRenderer>();
            StartCoroutine(coroutine = Coroutine());
        }

        private IEnumerator Coroutine()
        {
            renderer.sprite = AttackSprite;
            time = 0f;

            while(time < 1f)
            {
                Vector3 dest = (Player.transform.position - transform.position);
                transform.position += dest.magnitude < 1f ? Vector3.zero : dest.normalized * ChaseSpeed * Time.deltaTime;

                time += Time.deltaTime / (CastTime - SleepTime);
                yield return null;
            }
            time = 0f;
            renderer.sprite = SleepSprite;

            while(time < SleepTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
        }

    } 
}
