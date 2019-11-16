using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{

    public List<AudioClip> myClips;
    AudioSource[] aud;
    

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponentsInChildren<AudioSource>();
    }

    public void AddSoundClip(AudioClip myAud, string audioName)
    {
        myClips.Add(myAud);
        
    }

    public void PlayClip(string audioName)
    {
        for (int i = 0; i < myClips.Count; i++)
        {
           if (myClips[i] != null) if (myClips[i].name== audioName) Playing(i);
        }
    }

    public void PlayClip(string audioName, bool loopaudio)
    {
        for (int i = 0; i < myClips.Count; i++)
        {
            if (myClips[i].name == audioName) Playing(i, loopaudio);
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
