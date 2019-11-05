using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class Charge : ActionOverTime, IBaseGameEventListener<Void>
{
    public float ChargeTime;
    public float ChargeSpeed;
    public VoidEvent OnCollision;
    public PlayerVariable Player;
    private Entity entity;

    public override bool CanDoBoth()
    {
        return false;
    }

    public override IEnumerator Execute(Entity entity)
    {
        if (loop != null && !ISCOMPLETE) yield break;
        Debug.Log("start charge");
        this.entity = entity;
        OnCollision.AddListener(this);
        yield return loop = ChargeLoop();
    }

    public void OnEventRaised(Void data)
    {
        Debug.Log("Collision");
        Player.Value.health--;
        entity.StopCoroutine(loop);
        Reset();
        OnCollision.RemoveListener(this);
    }

    private IEnumerator ChargeLoop()
    {
        float progress = 0f;
        Vector3 start = entity.transform.position;
        Vector3 end = entity.transform.position + (Player.Value.transform.position - start).normalized * ChargeTime * ChargeSpeed;

        while (progress <= 1.0f)
        {
            entity.transform.position = Vector3.Lerp(start, end, progress);
            progress += Time.deltaTime / ChargeTime;

            yield return null;
        }
        Debug.Log("end charge");

    }
}
