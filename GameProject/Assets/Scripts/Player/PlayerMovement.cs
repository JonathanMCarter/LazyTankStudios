using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 100;
    public GameObject menu;
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed);
        if (Input.GetButtonDown("Fire2"))
        {
            menu.SetActive(true);
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
