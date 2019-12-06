using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSys : MonoBehaviour
{
    QuestLog QLog;
    Inventory inv;
    //public Sprite[] itemsprites;
    string FilePath;
    // Start is called before the first frame update

        public void Delete()
    {

        File.Delete(Application.persistentDataPath + "/SaveFile.txt");
    }


    public void Save()
    {

        var save = new SaveFile()
        {
            ID = QLog.ID,
            QuestTag = QLog.QuestTag,
            Status = QLog.Status,
            Collectables = QLog.Collectables,
            items = inv.items,
           // icons = inv.icons,
            Coins = inv.getCoins()
            
        };


        var bF = new BinaryFormatter();
        using (var filestream = File.Create(FilePath))
        {
            bF.Serialize(filestream, save);
          //  print("Has it saved");
        }

       // print("File saved");

    }


    public void Load() //this runs from Awake in QLog so will always be before Save function
    {
        QLog = GetComponent<QuestLog>();
        inv = FindObjectOfType<Inventory>();
        FilePath = Application.persistentDataPath + "/SaveFile.txt";
        if (File.Exists(FilePath))
        {

            SaveFile save;

            var bF = new BinaryFormatter();
            using (var fileStream = File.Open(FilePath, FileMode.Open))
            {
                save = (SaveFile)bF.Deserialize(fileStream);
              //  print("File Opened");
            }


            QLog.ID = save.ID;
            QLog.QuestTag = save.QuestTag;
            QLog.Status = save.Status;
            QLog.Collectables = save.Collectables;
            inv.addCoins(save.Coins);
            inv.items = save.items;
            //InvIcon();
            //inv.icons = save.icons;
           // print("Loaded");

        }
       // else print("File not found");



    }

    //void InvIcon()
    //{
        
    //    for (int i = 0; i < inv.items.Count; i++)
    //    {
    //        inv.icons.Add(itemsprites[inv.items[i]]);
    //    }
    //}
}
