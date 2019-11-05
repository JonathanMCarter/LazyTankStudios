using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
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
            bac.Init(transform.position, colliderSize, ProjectileSpeed, ProjectileRange, (Player.Value.transform.position - transform.position).normalized, ProjectileSprite);
            yield return null;
        }
    }
}
