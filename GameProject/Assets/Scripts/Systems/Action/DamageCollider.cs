using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public ColliderEvent Collider;
    public BoxCollider2D boxCollider;

    public void Awake()
    {
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = Vector2.one;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        Collider?.Raise(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Collider?.Raise(collision);
    }

}
