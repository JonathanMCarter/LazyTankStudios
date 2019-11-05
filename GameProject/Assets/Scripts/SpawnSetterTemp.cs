using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSetterTemp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Hero").transform.position = new Vector3(transform.position.x, transform.position.y, GameObject.Find("Hero").transform.position.z);
        // Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0);
        //GameObject.Find("Hero").GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x, transform.position.y));
    }
}
