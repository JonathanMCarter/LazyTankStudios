using UnityEngine;

public class SpawnSetterTemp : A
{
    // Start is called before the first frame update
    void Start()
    {
       Transform go = GameObject.Find("Hero").transform;
       go.position = new Vector3(transform.position.x, transform.position.y, go.position.z);
    }

}
