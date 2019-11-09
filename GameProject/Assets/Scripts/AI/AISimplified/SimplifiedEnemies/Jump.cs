﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public class Jump : Ability
    {
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float jumpDistance = 1f;

        private ActionOverTime aot;
        private Vector2 dest;
        private Vector2 start;
        private float time;

        public override void Awake()
        {
            base.Awake();
            aot = new ActionOverTime();
            aot.ActionDelegate = JumpExecute;
            ExpiryTime = (jumpDistance / jumpSpeed);
            aot.ExpiryTime = ExpiryTime;
        }

        public override IEnumerator Use()
        {
            if (AnimationClip != null) AnimationClip.Play();
            time = 0;
            start = transform.position;
            dest = start + ((Vector2)Player.transform.position - start).normalized * jumpDistance;
            yield return aot.Use();
        }

        public void JumpExecute()
        {
            time += Time.deltaTime;

            transform.position = Vector2.Lerp(start, dest, time / ExpiryTime);
        }
    } 
}