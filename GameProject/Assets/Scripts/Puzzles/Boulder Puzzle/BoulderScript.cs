using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    public float MoveDistance;
    public float MoveSpeed;
    private bool Move;  
    private float TotalDistance;
    private Vector2 Temp;
    public GateScript Gate;
    void Start()
    {
        TotalDistance = 0;
        Move = false;
        Temp = transform.position;
    }

    private bool DistanceTraveled()
    {
        if(TotalDistance >= MoveDistance)
        {
            Move = false;
            return false;
        }
        return true;
    }
    void Update()
    {
        if(Move)
        {
            Temp.x += MoveSpeed * Time.deltaTime;
            TotalDistance += Time.deltaTime;
        }
    }
    private void LateUpdate()
    {
        if (DistanceTraveled())
        {
            transform.position = Temp;
        }   
        else
        {
            Gate.AddBoulder();
            GetComponent<BoulderScript>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Move = true;
        }
    }
}
