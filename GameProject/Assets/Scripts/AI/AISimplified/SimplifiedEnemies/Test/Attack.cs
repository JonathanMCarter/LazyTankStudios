using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Attack : Ability
    {
        public GameObject projectilePrefab;
        public Vector2 colliderSize = Vector2.one * 0.5f;
        public float ProjectileSpeed;
        public float ProjectileRange;
        public Sprite ProjectileSprite;

        public void Awake()
        {
            CastTime = ProjectileRange / ProjectileSpeed;
        }

        public override void Interrupt(){}

        public override void Use()
        {
            BasicAttackCollider bac = Instantiate(projectilePrefab).GetComponent<BasicAttackCollider>();
            bac.Init(transform.position, colliderSize, ProjectileSpeed, ProjectileRange, (Player.transform.position - transform.position).normalized, ProjectileSprite);
            if(myAnimator != null)myAnimator.SetTrigger("Attack");
        }
    } 
}
