using UnityEngine;

public class AssignTransformVariable : MonoBehaviour
{
    public TransformVariable Target;
    public void Awake()
    {
        Target.Value = transform;
        Destroy(this);
    }
}
