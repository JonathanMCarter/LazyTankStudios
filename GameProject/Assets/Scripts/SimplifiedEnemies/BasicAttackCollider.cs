using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class BasicAttackCollider : MonoBehaviour
{
    public Vector2 colliderSize;
    public BoxCollider2D collider;
    public float projectileSpeed;
    public float projectileRange;
    public Vector2 direction;
    public Sprite sprite;

    private float timer;
    private float time;
    void Update()
    {
        if (time >= timer)
            Destroy(gameObject);
        transform.position += (Vector3)direction * projectileSpeed * Time.deltaTime;
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector2 startPos, Vector2 colliderSize, float speed, float range, Vector2 dir, Sprite sprite)
    {
        transform.position = startPos;
        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = colliderSize;
        GetComponent<SpriteRenderer>().sprite = sprite;
        projectileSpeed = speed;
        projectileRange = range;
        timer = projectileRange / projectileSpeed;
        direction = dir;
    }
}
