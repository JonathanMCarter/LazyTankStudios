using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINode : MonoBehaviour
{

    public Transform Pairing;
    public Vector2 Destination;
    public bool End;

    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        Destination = Pairing.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy At node");
            Vector2 Direction = new Vector2(Destination.x - pos.x, Destination.y - pos.y);
            collision.gameObject.GetComponent<NewAIMove>().Node(Direction);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (End)
        {
            if (collision.gameObject.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<NewAIMove>().LeaveNode();
            }
        }
    }
}
