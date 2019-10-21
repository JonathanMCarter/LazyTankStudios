using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase", menuName = "Abilities/Chase")]
public class Chase : ActionOverTime
{
    public float ChaseTime;
    public float ChaseSpeed;
    public PlayerVariable Player;
    private Entity entity;

    public override bool CanDoBoth()
    {
        return false;
    }

    public override IEnumerator Execute(Entity entity)
    {
        if (loop != null && !ISCOMPLETE) yield break;
        this.entity = entity;
        yield return loop = ChaseLoop();
    }

    private IEnumerator ChaseLoop()
    {
        float progress = 0f;

        while (progress <= 1.0f)
        {
            entity.transform.position += (Player.Value.transform.position - entity.transform.position).normalized * Time.deltaTime * ChaseSpeed;
            progress += Time.deltaTime / ChaseTime;

            yield return null;
        }
    }
}
