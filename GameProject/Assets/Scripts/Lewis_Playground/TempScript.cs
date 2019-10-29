using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("LevelOver");
            StartCoroutine(GameReset());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("LevelOver");
            StartCoroutine(GameReset());
        }
    }

    IEnumerator GameReset() //added temp by LC
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Main Menu");
        DoNotDes[] Gos = GameObject.FindObjectsOfType<DoNotDes>();
        foreach (DoNotDes go in Gos) if (go.gameObject != this.gameObject) Destroy(go.gameObject);
        yield return new WaitForSeconds(0);
        Destroy(this.gameObject);
    }
}
