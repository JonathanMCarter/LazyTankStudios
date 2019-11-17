using UnityEngine;
/*
 * Created by Toby Wishart
 * Last edit: 13/11/19
 * Reason: Fixing tears in the tilemap
 */
public class CameraMove : A
{

    //private Transform p;
    //Camera cam;

    //void Start()
    //{
    //    p = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    //    cam = GetComponent<Camera>();
    //}

    //void Update()
    //{
    //    if (cam.pixelHeight % 2 != 0) Debug.Log("Screen height is an odd number, expect the black lines");
    //    float scale = (8.0F/*The PPU of the assets*/ * Mathf.Max(1, Mathf.Min(((float)Screen.height / 108.0F/*The target height*/), ((float)Screen.width / 192.0F/*The target width*/))));
    //    cam.orthographicSize = ((float)Screen.height / 2F) / scale;
    //    if (GameObject.Find("Boundary"))
    //    {
    //        BoxCollider2D b = GameObject.Find("Boundary").GetComponent<BoxCollider2D>();
    //        BoxCollider2D camBox = GetComponent<BoxCollider2D>();
    //        //Adjust the camera box collider based on the size of the camera
    //        camBox.size = new Vector2(cam.orthographicSize * 2 * cam.aspect, cam.orthographicSize * 2);
    //        //Follow the player and stay within the boundaries
    //        transform.position = new Vector3(Mathf.Clamp(p.position.x, b.bounds.min.x + camBox.size.x / 2, b.bounds.max.x - camBox.size.x / 2), Mathf.Clamp(p.position.y, b.bounds.min.y + camBox.size.y / 2, b.bounds.max.y - camBox.size.y / 2), transform.position.z);
    //    }
    //    //Units per pixel
    //    float up = 1.0F / scale;
    //    //Snap camera movement to pixels
    //    transform.position = new Vector3(Mathf.Round(transform.position.x / up) * up, Mathf.Round(transform.position.y / up) * up, transform.position.z);
    //}
}
