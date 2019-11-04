using UnityEngine;


public class MoveAction : Action
{
    public Vector2 Target;
    public float Speed;

    public void Init(Vector2 target, float speed)
    {
        Target = target;
        Speed = speed;
        ActionDelegate = Move;
    }

    public void Move(MonoBehaviour mb, float deltaTime)
    {
        Vector3 dest = (Vector3)Target - mb.transform.position;
        mb.transform.position += dest.magnitude < .1f ? Vector3.zero : dest.normalized * deltaTime * Speed;
    }
}
