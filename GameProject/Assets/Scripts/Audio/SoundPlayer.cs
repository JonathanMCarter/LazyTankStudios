using System.Collections.Generic;
using UnityEngine;
public class SoundPlayer : A
{
    public List<AudioClip> myClips;
    public List<float> volumns, pitch;
    AudioSource[] aud;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        aud = GetComponentsInChildren<AudioSource>();
    }
    public void AddSoundClip(AudioClip myAud, /*float Volumn,*/ float Pitch)
    {
        myClips.Add(myAud);
       volumns.Add(1);
        pitch.Add(Pitch);
    }
    public void Play(string audioName, bool Loop = false)
    {
        for (int i = 0; i < myClips.Count; i++)
        {
            if (myClips[i] != null) if (myClips[i].name == audioName) Playing(i, Loop);
        }
    }
    private void Playing(int number, bool Loop)
    {
        for (int i = 1; i < aud.Length; i++)
        {
            if (!aud[i].isPlaying)
            {
                aud[i].loop = Loop;
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
