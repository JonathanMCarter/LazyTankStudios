using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    public List<AudioClip> myClips;
    public List<string> myNames;
    AudioSource[] aud;
    

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponentsInChildren<AudioSource>();
    }

    public void AddSoundClip(AudioClip myAud, string audioName)
    {
        myClips.Add(myAud);
        myNames.Add(audioName);
    }

    public void PlayClip(string audioName)
    {
        for (int i = 0; i < myNames.Count; i++)
        {
            if (myNames[i] == audioName) Playing(i);
        }
    }

    // Jonathan added this overload function so the clip volume could be changed
    public void PlayClip(string audioName, float Volume = 1)
    {
        for (int i = 0; i < myNames.Count; i++)
        {
            if (myNames[i] == audioName) Playing(i, Volume);
        }
    }

    // Jonathan added this overload function so the clip volume could be changed
    public void PlayClip(string audioName, float Volume = 1, bool Loop = false)
    {
        for (int i = 0; i < myNames.Count; i++)
        {
            if (myNames[i] == audioName) Playing(i, Volume);
        }
    }

    // Jonathan edited this function so the clip volume could be changed
    private void Playing(int number, float Volume = 1, bool Loop = false)
    {
        for (int i = 0; i < aud.Length; i++)
        {
            if (!aud[i].isPlaying)
            {
                aud[i].clip = myClips[number];
                aud[i].volume = Volume;
                aud[i].loop = Loop;
                aud[i].Play();
                return;

            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        aud.clip = myClips[0];
    //        aud.Play();
    //        print("Playing " + myNames[0]);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        aud.clip = myClips[1];
    //        aud.Play();
    //        print("Playing " + myNames[1]);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha0))
    //    {
    //        aud.pitch += 0.1f;
    //        print("Pitch up");
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha9))
    //    {
    //        aud.pitch -= 0.1f;
    //        print("Pitch Down");
    //    }
    //}
}
