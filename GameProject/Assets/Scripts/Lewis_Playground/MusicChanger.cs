using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public AudioClip myClip;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().transform.GetChild(0).GetComponent<AudioSource>().clip = myClip;
            FindObjectOfType<AudioManager>().transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
