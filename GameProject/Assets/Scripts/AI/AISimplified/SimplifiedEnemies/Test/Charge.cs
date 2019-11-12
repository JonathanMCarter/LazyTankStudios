using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Charge : Ability
    {
        public float ChannelTime;
        public float ChargeSpeed;
        private IEnumerator coroutine;
        private Vector3 dir;
        public void Awake()
        {
            CastTime = ChannelTime + CastTime;
        }

        public override void Interrupt()
        {
            if (coroutine == null) return;
            StopCoroutine(coroutine);
        }

        public override void Use()
        {
            if (coroutine != null)
                coroutine = null;
            dir = (Player.transform.position - transform.position).normalized;
            StartCoroutine(coroutine = Coroutine());
        }

        private IEnumerator Coroutine()
        {
            float time = 0f;
            while (time < ChannelTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            time = 0f;
            while(time <= 1f)
            {
                transform.position += dir * Time.deltaTime * ChargeSpeed;
                time += Time.deltaTime / CastTime;
                yield return null;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Player")
            {
                Debug.Log("Interrupt");
                Interrupt();
            }
        }
    } 
}
