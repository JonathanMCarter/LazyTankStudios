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

    public GameObject Boundary;

    public PlayerMovement Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>();
        SpawnRM3EM();
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

        if (Room2Em.Count != 0)
        {
            for (int i = 0; i < Room2Em.Count; i++)
            {
                if (!Room2Em[i].activeInHierarchy)
                {
                    Room2Em.RemoveAt(i);
                }
            }
        }

        if (Room3Em.Count != 0)
        {
            for (int i = 0; i < Room3Em.Count; i++)
            {
                if (!Room3Em[i].activeInHierarchy)
                {
                    Room3Em.RemoveAt(i);
                }
            }
        }

        Room1Cleared();
        Room2Cleared();
        Room3Cleared();
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

    public void SpawnRM3EM()
    {
        for (int i = 0; i < Room3Em.Count; i++)
        {
            Vector2 Pos = new Vector2();

            // This needs changing to work!!
            //Pos = new Vector2(Mathf.Clamp(Pos.x, -Boundary.GetComponent<BoxCollider2D>().bounds.center.x, Boundary.GetComponent<BoxCollider2D>().bounds.center.x), Mathf.Clamp(Pos.y, -Boundary.GetComponent<BoxCollider2D>().bounds.center.y, Boundary.GetComponent<BoxCollider2D>().bounds.center.y));

            Pos = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));

            Debug.Log(Pos);

            Instantiate(Room3Em[0], Pos, transform.rotation);
        }
    }
}
