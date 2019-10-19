using AI;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Spin", menuName = "Abilities/Spin")]
public class Spin : ActionOverTime
{
    public int DamageCooldown;
    public float SpinRadius;
    public PlayerVariable player;

    private float cooldown = 0f;
    private Entity entity;
    public override bool CanDoBoth()
    {
        return false;
    }

    public override IEnumerator Execute(Entity entity)
    {
        if (loop != null && !ISCOMPLETE) yield break;
        this.entity = entity;
        yield return loop = SpinLoop();
    }

    public IEnumerator SpinLoop()
    {
        if (AnimationClip != null) AnimationClip.Play();
        Debug.Log("StartSpin");
        float time = 0f;
        while(time <= ExpiryTime)
        {
            if ((entity.transform.position - player.Value.transform.position).magnitude < SpinRadius) Damage();
            time += Time.deltaTime;
            cooldown = (cooldown - Time.deltaTime < 0) ? 0 : cooldown-=Time.deltaTime;
            yield return null;
        }
        Debug.Log("End Spin");
    }

    public void Damage()
    {
        if (cooldown != 0) return;
        player.Value.Health--;
        cooldown = DamageCooldown;
    }
}
