using System.Collections;
using UnityEngine;
public class NewAIMove: A {
 public float MoveSpeed, RotateSpeed, Direction;
 float WaitTime, TurnTime;
 public bool boss;
 public Transform me;
 Transform Player;
 Rigidbody2D MyRigid;
 public Vector2 Paramiers, WaitVarables;
 public Vector3 offset;
 bool SeenPlayer, Turn, TurnCount, ToggleDirection, hit;
 public SpriteRenderer[] Hearts;
 public int Health;
 public float DamageCD = 0.3 f;
 PlayerMovement player;
 Transform PlayerPos;
 static public int currBoss = 0;
 SoundPlayer sp;
 void Start() {
  TurnCount = true;
  MyRigid = GetComponent < Rigidbody2D > ();
  Health = Hearts.Length;
  PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
  sp = FindObjectOfType < SoundPlayer > ();
 }
 void Update() {
  me.transform.position = transform.position + offset;
  if (SeenPlayer) RunToPlayer();
  else RandomWander();
 }
 void FixedUpdate() {
  if (ToggleDirection || SeenPlayer) {
   ToggleDirection = true;
   if (!SeenPlayer) MyRigid.angularVelocity = 0;
   MyRigid.velocity = transform.up * MoveSpeed;
  } else MyRigid.velocity = transform.up * -MoveSpeed;
  if (Turn) {
   if (Direction < 50) transform.Rotate(0, 0, RotateSpeed);
   else if (Direction >= 50) transform.Rotate(0, 0, -RotateSpeed);
  }
 }
 void RandomWander() {
  if (TurnCount) {
   TurnTime -= Time.deltaTime;
   Direction = Random.Range(1, 100);
   WaitTime = Random.Range(WaitVarables.x, WaitVarables.y);
   Turn = false;
   if (TurnTime <= 0) {
    TurnCount = false;
    Turn = true;
   }
  } else {
   TurnTime = Random.Range(Paramiers.x, Paramiers.y);
   WaitTime -= Time.deltaTime;
   if (WaitTime <= 0) {
    TurnCount = true;
   }
  }
 }
 void RunToPlayer() {
  Vector2 Direction = new Vector2(PlayerPos.position.x - transform.position.x, PlayerPos.position.y - transform.position.y);
  transform.up = Direction;
 }
 void OnTriggerEnter2D(Collider2D collision) {
  if (collision.gameObject.CompareTag("Player")) {
   Player = collision.gameObject.transform;
   SeenPlayer = true;
  }
 }
 void OnTriggerExit2D(Collider2D collision) {
  if (collision.gameObject.CompareTag("Player")) {
   SeenPlayer = false;
   ToggleDirection = false;
   Player = null;
  }
 }
 void OnCollisionEnter2D(Collision2D collision) {
  if (ToggleDirection) ToggleDirection = false;
  else ToggleDirection = true;
  if (collision.gameObject.tag == "Bullet") {
   Destroy(collision.gameObject);
   if (Health > 0) Hearts[Health - 1].gameObject.SetActive(false);
   Health -= 1;
   sp.Play("Take_Damage_3");
  }
  if (Health <= 0) {
   if (boss) Quest.boss[currBoss] = true;
   gameObject.SetActive(false);
  }
  if (collision.gameObject.tag == "Sword") {
   if (Health > 0) Hearts[Health - 1].gameObject.SetActive(false);
   Health -= 1;
   sp.Play("Take_Damage_3");
  }
  if (Health <= 0) {
   sp.Play("Death_3");
   gameObject.SetActive(false);
  }
 }
}