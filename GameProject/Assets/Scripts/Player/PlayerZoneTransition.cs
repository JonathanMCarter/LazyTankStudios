using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Owner: Toby Wishart
 * Last edit: 03/11/19
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
    AsyncOperation asyncLoad;

    float sX, sY;

    public string Destination;
    string[] destSplit;
    bool transitioning = false;

    private AudioManager audioManager;

    public void Start()
    {
        audioManager=GameObject.FindObjectOfType<AudioManager>();
    }

    public void StartTransition()
    {
        destSplit = Destination.Split(':');
        GetComponent<SpriteRenderer>().flipX = false;
        //Camera.main does not work here
        cameraBox = GameObject.Find("Main Camera").GetComponent<BoxCollider2D>();
        pAnim = GetComponent<Animator>();
        sX = pAnim.GetFloat("SpeedX");
        sY = pAnim.GetFloat("SpeedY");
        transform.position = new Vector3(cameraBox.bounds.min.x - 5, cameraBox.bounds.center.y, transform.position.z);
        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>()) { box.enabled = false; }
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false);
        transitioning = true;
        //audioManager.Play("Door");
        asyncLoad = SceneManager.LoadSceneAsync(destSplit[0]);
        asyncLoad.allowSceneActivation = false;
    }

    private void Update()
    {
        if (transitioning)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * 10;
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
        //transform.position = Destination.position;
        pAnim.SetFloat("SpeedX", sX);
        pAnim.SetFloat("SpeedY", sY);
        //Camera.main does not work here
        GameObject.Find("Main Camera").GetComponent<CameraMove>().enabled = true;
        asyncLoad.allowSceneActivation = true;
        StartCoroutine(waitUntilLoaded());

    }

    private IEnumerator waitUntilLoaded()
    {
        yield return new WaitUntil(() => asyncLoad.isDone);
        Vector3 newPos = GameObject.Find(destSplit[1]).transform.position;
        //Preserve the player's Z position
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>()) box.enabled = true;
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(true);
        Animator a = GameObject.Find("ZoneFadeScreen").GetComponent<Animator>();
        a.SetBool("out", false);
        a.SetBool("in", true);
    }
}
