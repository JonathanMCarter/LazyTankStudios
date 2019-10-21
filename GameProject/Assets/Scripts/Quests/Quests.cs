using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using AI;

[System.Serializable]
public class Quest
{
    public enum Type { Return,NonReturn};
    public Type type;



    public GameObject NPCToReturnTo;
    public DialogueFile Dialogue;



    public bool KillRequest, CollectRequest, DeliverRequest;
    public List<GameObject> Kills;
    public GameObject[] Collectible;
    public int[] CollectibleAmmount;
    public int DeliverGold;



    public enum Reward { Items,Gold}
    public Reward reward;
    public GameObject[] Items;
    public Sprite[] ItemsSprites;
    public int GoldReceived;




    public enum Status { Available,OnGoing,Completed};
    public Status status;

    
}











public class Quests : MonoBehaviour
{
    
    public Quest Quest;
    private DialogueScript ds;
    private void Start()
    {
        ds = GameObject.Find("DialogueHandler").GetComponent<DialogueScript>();
        for(int i=0;i<Quest.Items.Length;i++)
            Quest.Items[i].GetComponent<SpriteRenderer>().sprite = Quest.ItemsSprites[i];
    }

    private void Update()
    {
        if (Quest.Kills.Count > 0)
            foreach (GameObject enemy in Quest.Kills)
            {
                if (enemy.GetComponent<AIMovement>().Health <= 0)
                {
                    Quest.Kills.Remove(enemy);
                    Destroy(enemy);
                }
            }
        else print("IT WORKS");

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.name == "Hero")
        {
            ds.ChangeFile(Quest.Dialogue);
            Quest.status = Quest.Status.OnGoing;
        }
    }

}
