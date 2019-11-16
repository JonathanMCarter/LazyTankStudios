using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzleThingy : A
{

    public List<GameObject> Room1Em,Room2Em,Room3Em,RoomItems;


    public List<bool> ItemSpawned;

    public GameObject Boundary,Door;


    PlayerMovement Player;

    public bool Rooms12Cleared, Em3Spawned,PuzzleComplete;
    int ItemsReturned; //Added by LC as temp


    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>();
    }

    public void ItemReturn() //Added by LC as temp
    {
        ItemsReturned++;
        if (ItemsReturned == 3) Door.SetActive(false); //should also play SFX here
    }

    // Update is called once per frame
    void Update()
    {
        if (Room1Em.Count != 0)
        {
            for (int i = 0; i < Room1Em.Count; i++)
            {
                if (!Room1Em[i].activeInHierarchy) Room1Em.RemoveAt(i);

            }
        }

        if (Room2Em.Count != 0)
        {
            for (int i = 0; i < Room2Em.Count; i++)
            {
                if (!Room2Em[i].activeInHierarchy) Room2Em.RemoveAt(i);

            }
        }

        if (Em3Spawned)
        {
            if (Room3Em.Count != 0)
            {
                for (int i = 0; i < Room3Em.Count; i++)
                {
                    if (!Room3Em[i].activeInHierarchy) Room3Em.RemoveAt(i);

                }
            }
        }

        Room1Cleared();
        Room2Cleared();


        if ((Room1Em.Count == 0) && (Room2Em.Count == 0))
        {
            Rooms12Cleared = true;

            if (Em3Spawned)Room3Cleared();

        }

        if (PuzzleComplete) Door.SetActive(false);

    }

    public void Room1Cleared()
    {
        if ((Room1Em.Count == 0) && (!ItemSpawned[0]))
        {
           // Debug.Log("Room 1 Cleared");
            Instantiate(RoomItems[0], Player.gameObject.transform.position, Player.gameObject.transform.rotation);
            ItemSpawned[0] = true;
        }
    }

    public void Room2Cleared()
    {
        if ((Room2Em.Count == 0) && (!ItemSpawned[1]))
        {
            Instantiate(RoomItems[1], Player.gameObject.transform.position, Player.gameObject.transform.rotation);
            ItemSpawned[1] = true;
        }
    }

    public void Room3Cleared()
    {
        if ((Room3Em.Count == 0) && (!ItemSpawned[2]))
        {
           // Debug.Log("Room 3 Cleared");
            Instantiate(RoomItems[2], Player.gameObject.transform.position, Player.gameObject.transform.rotation);
            ItemSpawned[2] = true;
        }
    }

    public void SpawnRM3EM()
    {
        if (!Em3Spawned)
        {
            for (int i = 0; i < Room3Em.Count; i++)
            {
                Vector2 Pos = new Vector2();

                // This needs changing to work!!
                //Pos = new Vector2(Mathf.Clamp(Pos.x, -Boundary.GetComponent<BoxCollider2D>().bounds.center.x, Boundary.GetComponent<BoxCollider2D>().bounds.center.x), Mathf.Clamp(Pos.y, -Boundary.GetComponent<BoxCollider2D>().bounds.center.y, Boundary.GetComponent<BoxCollider2D>().bounds.center.y));

                Pos = new Vector3(Random.Range(-62.5f, -33.1f), Random.Range(-39.6f, -8f), -0.2f);//LC added Zaxis as was spawning behind floor

                Debug.Log(Pos);

                GameObject Go = Instantiate(Room3Em[0], Pos, transform.rotation);
                Room3Em[0] = Go;

                Em3Spawned = true;
            }
        }
    }
}
