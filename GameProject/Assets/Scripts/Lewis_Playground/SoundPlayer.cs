using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    public List<AudioClip> myClips;
    public List<string> myNames;
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void AddSoundClip(AudioClip myAud, string audioName)
    {
        myClips.Add(myAud);
        myNames.Add(audioName);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            aud.clip = myClips[0];
            aud.Play();
            print("Playing " + myNames[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            aud.clip = myClips[1];
            aud.Play();
            print("Playing " + myNames[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            aud.pitch += 0.1f;
            print("Pitch up");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            aud.pitch -= 0.1f;
            print("Pitch Down");
        }
    }
}
