using UnityEngine;
public class FireTrap : A{
public int Damage;
public float ActiveSeconds, WaitSeconds, damageTime;
public Sprite ActiveSprite, WaitSprite;
float Timer, damageTimer;
bool Active, DamageTrigger;
BoxCollider2D MyCol;
SpriteRenderer MyRend;
PlayerMovement MyPlay;
void Start(){
Timer = 0f;
Active = false;
MyCol = GetComponent<BoxCollider2D>();
MyRend = GetComponent<SpriteRenderer>();
MyPlay = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();}
void Update(){
Timer += Time.deltaTime;
if (!Active){
if (Timer >= ActiveSeconds){
Active = true;
Timer = 0f;
MyCol.enabled = true;
MyRend.sprite = ActiveSprite;}}
else{
if (Timer >= WaitSeconds){
Active = false;
Timer = 0f;
MyCol.enabled = false;
MyRend.sprite = WaitSprite;}}
if (DamageTrigger){
damageTimer += Time.deltaTime;
if (damageTimer >= damageTime){
MyPlay.TakeDamage(Damage);}}}
void OnTriggerEnter2D(Collider2D collision){
if (collision.gameObject.CompareTag("Player")){
DamageTrigger = true;
MyPlay.TakeDamage(Damage);}}
void OnTriggerExit2D(Collider2D collision){
if (collision.gameObject.CompareTag("Player")){
DamageTrigger = false;}}}