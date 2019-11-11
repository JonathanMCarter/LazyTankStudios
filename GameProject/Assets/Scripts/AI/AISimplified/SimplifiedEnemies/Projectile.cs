using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
     /**
        *Edit by Andreas Kraemer
        *Last Edit: 11/11/19
        *
        *Set Animation Parameters
        */
    public class Projectile : Ability
    {
        public GameObject projectilePrefab;
        public Vector2 colliderSize = Vector2.one * 0.5f;
        public float ProjectileSpeed;
        public float ProjectileRange;
        public Sprite ProjectileSprite;
        public override IEnumerator Use()
        {
            BasicAttackCollider bac = Instantiate(projectilePrefab).GetComponent<BasicAttackCollider>();
            bac.Init(transform.position, colliderSize, ProjectileSpeed, ProjectileRange, (Player.transform.position - transform.position).normalized, ProjectileSprite);
            myAnimator.SetTrigger("Attack");
            yield return null;
        }
    }
}
