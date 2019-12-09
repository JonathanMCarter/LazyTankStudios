using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class SaveSys : A{
QuestLog QLog;
Inventory inv;
string FilePath;
public void Delete(){
File.Delete(Application.persistentDataPath + "/SaveFile.txt");}
public void Save(){
var save = new SaveFile(){
ID = QLog.ID,
QuestTag = QLog.QuestTag,
Status = QLog.Status,
Collectables = QLog.Collectables,
items = inv.items,
Coins = inv.getCoins(),
Pots = inv.pots};
var bF = new BinaryFormatter();
using (var filestream = File.Create(FilePath)){
bF.Serialize(filestream, save);}}
public void Load(){
QLog = GetComponent<QuestLog>();
inv = FindObjectOfType<Inventory>();
FilePath = Application.persistentDataPath + "/SaveFile.txt";
if (File.Exists(FilePath)){
SaveFile save;
var bF = new BinaryFormatter();
using (var fileStream = File.Open(FilePath, FileMode.Open)){
save = (SaveFile)bF.Deserialize(fileStream);}
QLog.ID = save.ID;
QLog.QuestTag = save.QuestTag;
QLog.Status = save.Status;
QLog.Collectables = save.Collectables;
Inventory.Coins = save.Coins;
inv.addCoins(0);
inv.items = save.items;
inv.pots = save.Pots;}}}