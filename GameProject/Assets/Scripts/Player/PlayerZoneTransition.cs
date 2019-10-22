using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Owner: Toby Wishart
 * Last edit: 20/10/19
 * 
 * Script for moving the player to the destination of the gate and move the player across the screen
 *
 *
 * Also edited by:Andreas Kraemer
 * Last edit: 21/10/19
 *
 * Added sound effects
 *
 */
public class PlayerZoneTransition : MonoBehaviour
{

    BoxCollider2D cameraBox;

    Animator pAnim;

    float sX, sY;

    public Transform Destination;
    bool transitioning = false;

    private AudioManager audioManager;

    public void Start()
    {
        audioManager=GameObject.FindObjectOfType<AudioManager>();
    }

    public void StartTransition()
    {
        cameraBox = Camera.main.GetComponent<BoxCollider2D>();
        pAnim = GetComponent<Animator>();
        sX = pAnim.GetFloat("SpeedX");
        sY = pAnim.GetFloat("SpeedY");
        transform.position = new Vector3(cameraBox.bounds.min.x - 5, cameraBox.bounds.center.y);
        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
        {
            box.enabled = false;
        }
        transitioning = true;
        audioManager.Play("Door");
    }

    private void Update()
    {
        if (transitioning)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * 5;
            pAnim.SetFloat("SpeedX", 5);
            pAnim.SetFloat("SpeedY", 0);
            if (transform.position.x > cameraBox.bounds.max.x + 5)
                FinishTransition();
        }
    }

    void FinishTransition()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transitioning = false;
        transform.position = Destination.position;
        pAnim.SetFloat("SpeedX", sX);
        pAnim.SetFloat("SpeedY", sY);
        Camera.main.GetComponent<CameraMove>().enabled = true;
        Animator a = GameObject.Find("ZoneFadeScreen").GetComponent<Animator>();
        a.SetBool("out", false);
        a.SetBool("in", true);
        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
        {
            box.enabled = true;
        }
    }
}
