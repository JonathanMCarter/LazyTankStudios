using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Projectile projectile;

    private float speed;
    private Vector2 start;

    public void Init()
    {
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = Vector2.one;
        if(projectile != null)
        {
            gameObject.AddComponent<SpriteRenderer>().sprite = projectile.Sprite;
            speed = projectile.speed;
        }
        start = transform.position;
    }

    public void Update()
    {
        if (projectile == null) return;
        if ((start - (Vector2)transform.position).magnitude > projectile.Range ) Destroy(gameObject);
        transform.position += (Vector3)projectile.dir * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement p = collision.gameObject.GetComponent<PlayerMovement>();
        if (p != null) p.health--;
        Destroy(gameObject);
    }
}
