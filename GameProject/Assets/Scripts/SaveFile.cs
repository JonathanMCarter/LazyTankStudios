
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile 
{
    public List<int> ID;
    public List<string> QuestTag;
    public List<Quest.Status> Status;
    public List<int> Collectables;
   // public List<Sprite> icons;
    public List<int> items;
    public int Coins;
}
