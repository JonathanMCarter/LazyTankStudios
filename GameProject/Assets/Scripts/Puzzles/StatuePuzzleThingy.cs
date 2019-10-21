using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzleThingy : MonoBehaviour
{

    public List<GameObject> Room1Em;
    public List<GameObject> Room2Em;

    public List<GameObject> RoomItems;

    public PlayerMovement Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Room1Cleared()
    {
        if (Room1Em.Count == 0)
        {
            GameObject Go = Instantiate(RoomItems[0], Player.gameObject.transform.position, Player.gameObject.transform.rotation);
        }
    }
}
