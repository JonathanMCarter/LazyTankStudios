using UnityEngine;
public class sign : A{
TalkScript ts;
DialogueScript ds;
void Start(){
ts = GetComponent<TalkScript>();
ds = F<DialogueScript>();}
void OnTriggerExit2D(Collider2D collision){
if (ts.isItTalking()){
ds.Input();}}}