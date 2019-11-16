using UnityEngine;

/*
 * Created by Toby Wishart
 * Last edit: 11/10/19
 */
public class Boundaries : A
{
    private Transform p;
    private BoxCollider2D box;

    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (p)
        {

            //If outside the parent boundary the camera boundary is disabled
            if (box.bounds.min.x < p.position.x && p.position.x < box.bounds.max.x && box.bounds.min.y < p.position.y && p.position.y < box.bounds.max.y)
                transform.GetChild(0).gameObject.SetActive(true);
            else
                transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            p = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

}
