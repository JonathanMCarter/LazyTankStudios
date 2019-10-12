using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Created by Toby Wishart
 * Last edit: 11/10/19
 */
public class CameraMove : MonoBehaviour
{

    private Transform p;

    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (GameObject.Find("Boundary"))
        {
            BoxCollider2D b = GameObject.Find("Boundary").GetComponent<BoxCollider2D>();
            BoxCollider2D cam = GetComponent<BoxCollider2D>();
            //Adjust the camera box collider based on the size of the camera
            cam.size = new Vector2((Camera.main.orthographicSize * Screen.width / Screen.height) * 2, Camera.main.orthographicSize * 2);
            //Follow the player and stay within the boundaries
            transform.position = new Vector3(Mathf.Clamp(p.position.x, b.bounds.min.x + cam.size.x / 2, b.bounds.max.x - cam.size.x / 2), Mathf.Clamp(p.position.y, b.bounds.min.y + cam.size.y / 2, b.bounds.max.y - cam.size.y / 2), transform.position.z);
        }
    }
}
