using System.Collections.Generic;
using UnityEngine;
public class QuestLog : A{
public List<int> ID;
public List<string> QuestTag;
public List<Quest.Status> Status;
public List<int> Collectables;
public bool Check(int QId, string Qtag){
for (int i = 0; i < ID.Count; i++){{
if (ID[i] == QId){
if (QuestTag[i] == Qtag){
if (Status[i] == Quest.Status.Completed){
return true;}}}}}
return false;}
public bool Collect(int QId, string Qtag){
for (int i = 0; i < ID.Count; i++){{
if (ID[i] == QId){
if (QuestTag[i] == Qtag){
if (Collectables[i] > 0){
Collectables[i]--;
SetQuest();
return true;}}}}}
return false;}
public void SetQuest(){
Quest[] Q = FindObjectsOfType<Quest>();
for (int i = 0; i < ID.Count; i++){
foreach (Quest q in Q){
if (ID[i] == q.ID){
if (QuestTag[i] == q.QuestTag){
q.status = Status[i];
q.ItemToCollect = Collectables[i];
q.killsNeeded = Collectables[i];}}}}}
public void SaveQuest(int id, string questtag, Quest.Status status, int collect, int kills){
for (int i = 0; i < ID.Count; i++){
if (ID[i] == id){
if (QuestTag[i] == questtag){
if (Status[i] != status) {
Status[i] = status;
GetComponent<SaveSys>().Save();}
return;}}}
ID.Add(id);
QuestTag.Add(questtag);
Status.Add(status);
if (collect > 0) Collectables.Add(collect); else Collectables.Add(kills);
GetComponent<SaveSys>().Save();}
void Awake(){
GetComponent<SaveSys>().Load();}}