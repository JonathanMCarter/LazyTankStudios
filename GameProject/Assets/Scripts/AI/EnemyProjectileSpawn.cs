using UnityEngine;
public class EnemyProjectileSpawn : A
{NewAIMove AIM;
public GameObject P;
public float _T;
float T;
bool F;
void Start()
{AIM = G<NewAIMove>();
T = _T;}
void Update()
{Timer();}
void FixedUpdate(){
if(F)
{Instantiate(P, transform.position , transform.rotation);
AIM.myAnim.SetTrigger("Attack");
F = false;}}
void Timer(){
if (T <= 0 && AIM.SeenPlayer){
F = true;
T = _T;}
else
T -= Time.deltaTime;}}