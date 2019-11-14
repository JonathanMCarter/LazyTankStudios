using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINode : MonoBehaviour
{

    public Transform Pairing;
    public Vector2 Destination;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        Destination = Pairing.position;
    }
    public Vector2 Directions()
    {
        Vector2 Direction = new Vector2(Destination.x - pos.x, Destination.y - pos.y);
        return Direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "NewEnemy")
        {
            Debug.Log("Enemy At node");
            pos = collision.gameObject.GetComponent<Transform>().position;
        }
    }
}
