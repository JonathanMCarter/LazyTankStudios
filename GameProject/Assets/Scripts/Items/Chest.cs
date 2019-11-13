using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<GameObject> contents;

    public void open()
    {
        GetComponent<Animator>().SetTrigger("open");
        foreach (GameObject g in contents)
        {
            GameObject newG = Instantiate(g);
            //Scatter the items around the chest randomly
            newG.transform.position = new Vector3(transform.position.x + Random.Range(-1F, 1F), transform.position.y + Random.Range(-1F, 1F), transform.position.z-1);
        }
        //Disable interaction so items don't duplicate
        GetComponent<Interact>().Enabled = false;
    }

}
