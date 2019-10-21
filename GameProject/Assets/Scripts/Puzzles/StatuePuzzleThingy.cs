using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzleThingy : MonoBehaviour
{

    public List<GameObject> Room1Em;
    public List<GameObject> Room2Em;
    public List<GameObject> Room3Em;

    public List<GameObject> RoomItems;
    public List<bool> ItemSpawned;

    public PlayerMovement Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Room1Em.Count != 0)
        {
            for (int i = 0; i < Room1Em.Count; i++)
            {
                if (!Room1Em[i].activeInHierarchy)
                {
                    Room1Em.RemoveAt(i);
                }
            }
        }

        Room1Cleared();
    }

    public void Room1Cleared()
    {
        if ((Room1Em.Count == 0) && (!ItemSpawned[0]))
        {
            Debug.Log("s,dfjklsdahjufhsuodf");
            Instantiate(RoomItems[0], Player.gameObject.transform.position, Player.gameObject.transform.rotation);
            ItemSpawned[0] = true;
        }
    }

    public void Room2Cleared()
    {
        if ((Room2Em.Count == 0) && (!ItemSpawned[1]))
        {
            Debug.Log("s,dfjklsdahjufhsuodf");
            Instantiate(RoomItems[1], Player.gameObject.transform.position, Player.gameObject.transform.rotation);
            ItemSpawned[1] = true;
        }
    }

    public void Room3Cleared()
    {
        if ((Room3Em.Count == 0) && (!ItemSpawned[2]))
        {
            Debug.Log("s,dfjklsdahjufhsuodf");
            Instantiate(RoomItems[2], Player.gameObject.transform.position, Player.gameObject.transform.rotation);
            ItemSpawned[2] = true;
        }
    }
}
