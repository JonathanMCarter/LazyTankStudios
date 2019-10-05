using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
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
        //If outside the parent boundary the camera boundary is disabled
        if (box.bounds.min.x < p.position.x && p.position.x < box.bounds.max.x && box.bounds.min.y < p.position.y && p.position.y < box.bounds.max.y)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }
}
