using UnityEngine;


/*
 * Created by: Lewis Cleminson
 * Date: 22.10.19
 * 
 * Temp script for collecting puzzle pieces (not going to inventory)
 * 
 * 
 * */


public class PuzzleComponent : A
{
    InputManager IM;
    bool IsCarried;
    GameObject Player;
    static bool IsCarrying;

    void Start()
    {
        IM = FindObjectOfType<InputManager>();
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject GO = collision.gameObject;
        if (GO.CompareTag(transform.gameObject.tag))//If tag matches that of the object
        {
            FindObjectOfType<StatuePuzzleThingy>().ItemReturn();
           GO.GetComponent<BoxCollider2D>().enabled = false;
            GO.transform.GetChild(0).gameObject.SetActive(true);
            IsCarrying = false;
            FindObjectOfType<SoundPlayer>().Play("Victory");
            gameObject.SetActive(false);
        }
        if (IsCarried) return;
        if (GO.CompareTag("Player") && IM.Button_A() && !IsCarrying)
        {
            Player = collision.gameObject;
            IsCarried = true;
            IsCarrying = true;
        }
    }

    void Update()
    {
        if (IsCarried) transform.position = Player.transform.position + new Vector3(0, 1, 0);
    }

}
