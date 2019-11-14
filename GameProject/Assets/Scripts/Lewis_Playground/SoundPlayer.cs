using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    public void PlayClip(string audioName, bool loopaudio)
    {
        for (int i = 0; i < myNames.Count; i++)
        {
            Debug.Log("calling");
            if (myNames[i] == audioName) Playing(i, loopaudio);
        }
    }


    // Jonathan edited this function so the clip could be looped for music
    private void Playing(int number, bool Loop = false)
    {
        for (int i = 0; i < aud.Length; i++)
        {
            if (!aud[i].isPlaying)
            {
                aud[i].loop = Loop;
                aud[i].clip = myClips[number];
                aud[i].Play();
                return;
            }
        }
    }
}
