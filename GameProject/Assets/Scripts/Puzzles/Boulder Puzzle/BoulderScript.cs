using UnityEngine;

public class BoulderScript : A
{
    public float MoveDistance,MoveSpeed;
    bool Move;  
    float TotalDistance;
    Vector2 Temp;
    public GateScript Gate;


    void Start()
    {
        TotalDistance = 0;
        Move = false;
        Temp = transform.position;
    }

    bool DistanceTraveled()
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

    void LateUpdate()
    {
        if (DistanceTraveled()) transform.position = Temp; 
        else
        {
            Gate.AddBoulder();
            GetComponent<BoulderScript>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) Move = true;

    }
}
