using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasicAttack : Action
{
    public Projectile Projectile;
    public GameObject prefab;
    public PlayerVariable player;
    public Vector2 ProjectileSize;

    public override bool CanDoBoth()
    {
        return false;
    }

    public override IEnumerator Execute(Entity mb)
    {
        Debug.Log(player.Value);
        Projectile.dir = (player.Value.transform.position - mb.transform.position).normalized;
        GameObject go = Instantiate(prefab);
        go.transform.position = mb.transform.position + 0.5f * (Vector3)Projectile.dir;
        DamageCollider dc = go.GetComponent<DamageCollider>();
        dc.projectile = Projectile;
        dc.Init();
        dc.boxCollider.size = ProjectileSize;

        ISCOMPLETE = true;
        yield return null;
    }

    public override bool IsComplete()
    {
        return ISCOMPLETE && Reset();
    }

    protected override bool Reset()
    {
        return !(ISCOMPLETE = false);
    }
}
