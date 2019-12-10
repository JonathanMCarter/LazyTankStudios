using UnityEngine;
public class EnemyProjectileMove : A{
public float S;
Rigidbody2D R;
void Start()
{R = G<Rigidbody2D>();}
private void FixedUpdate()
{R.velocity = transform.up * S;}
void OnCollisionEnter2D(Collision2D collision){
Destroy(gameObject);}}