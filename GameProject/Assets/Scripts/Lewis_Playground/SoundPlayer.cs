﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{

    public List<AudioClip> myClips;
    public List<float> volumns, pitch;
    AudioSource[] aud;


    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponentsInChildren<AudioSource>();
    }

    public void AddSoundClip(AudioClip myAud, float Volumn, float Pitch)
    {
        myClips.Add(myAud);
       volumns.Add(Volumn);
        pitch.Add(Pitch);

    }

    public void PlayClip(string audioName)
    {
        for (int i = 0; i < myClips.Count; i++)
        {
            if (myClips[i] != null) if (myClips[i].name == audioName) Playing(i);
        }
    }

    private void Playing(int number)
    {
        for (int i = 1; i < aud.Length; i++)
        {
            if (!aud[i].isPlaying)
            {
                aud[i].clip = myClips[number];
                aud[i].volume = volumns[number];
                aud[i].pitch = pitch[number];
                aud[i].Play();
                return;
            }
        }
    }

    public void PlayMusic(string audioName)
    {
        for (int i = 0; i < myClips.Count; i++)
        {
            if (myClips[i] != null) if (myClips[i].name == audioName)
                {
                   aud[0].clip = myClips[i]; aud[0].Play();
                }
        }
    }
}
