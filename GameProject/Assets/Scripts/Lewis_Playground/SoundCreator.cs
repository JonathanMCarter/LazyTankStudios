﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundCreator : MonoBehaviour
{
   public enum Notes
    {
        Silence,
        C,
        Db,
        D,
        Eb,
        E,
        F,
        Gb,
        G,
        Ab,
        A,
        Bb,
        B,
        C1,
        Db1,
        D1,
        Eb1,
        E1,
        F1,
        Gb1,
        G1,
        Ab1,
        A1,
        Bb1,
        B1,
        C2,
        Db2,
        D2,
        Eb2,
        E2,
        F2,
        Gb2,
        G2,
        Ab2,
        A2,
        Bb2,
        B2,
        C3,
        Db3,
        D3,
        Eb3,
        E3,
        F3,
        Gb3,
        G3,
        Ab3,
        A3,
        Bb3,
        B3,
        C4,
        Db4,
        D4,
        Eb4,
        E4,
        F4,
        Gb4,
        G4,
        Ab4,
        A4,
        Bb4,
        B4,
        C5,
        Db5,
        D5,
        Eb5,
        E5,
        F5,
        Gb5,
        G5,
        Ab5,
        A5,
        Bb5,
        B5,


    }
    public string AudioName;
    public int AudioLength;
    public int BeatsPerSecond;
    int position = 0;
    public int samplerate = 44100;
    public float[] frequency;
    float frequency_current;
    public Notes[] MyTune;



    // Start is called before the first frame update
    void Awake()
    {
        frequency = new float[MyTune.Length];
        for (int i = 0; i < MyTune.Length; i++)
        {
            frequency[i] = (int)MyTune[i];
        }
        CalculateFrequencies();
        if (BeatsPerSecond == 0) BeatsPerSecond = 1;
        AudioLength = BeatsPerSecond;
        frequency_current = frequency[0];
        AudioClip AClip = AudioClip.Create("MyClip", samplerate / AudioLength, 1, samplerate, false, OnAudioRead, OnAudioSetPosition);
        AudioClip TheClip = null;
        for (int i = 1; i < frequency.Length; i++)
        {
            frequency_current = frequency[i];
            AudioClip myClip = AudioClip.Create("MyClip", samplerate / AudioLength, 1, samplerate, false, OnAudioRead, OnAudioSetPosition);
            TheClip = MergeClips(AClip, myClip);
            AClip = TheClip;
        }

        GetComponentInParent<SoundPlayer>().AddSoundClip(TheClip, AudioName);
    }

    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * frequency_current * position / samplerate));
            position++;
            count++;
        }
    }

    void OnAudioSetPosition(int newPosition)
    {
        position = newPosition;
    }

    AudioClip MergeClips(AudioClip clip1, AudioClip clip2)
    {
        int length = 0;
        float[] buffer = new float[clip1.samples];
        float[] data = new float[clip1.samples + clip2.samples];

        clip1.GetData(buffer, 0);
        buffer.CopyTo(data, length);
        length += clip1.samples;

        float[] buffer2 = new float[clip2.samples];

        clip2.GetData(buffer2, 0);
        buffer2.CopyTo(data, length);
        length = clip1.samples + clip2.samples;

        AudioClip newclip = AudioClip.Create("CombinedClip", length, 1, samplerate, false);
        newclip.SetData(data, 0);
        return newclip;
    }

    void CalculateFrequencies()
    {
        float[] temp = frequency;
        for (int i = 0; i < frequency.Length; i++)
        {
            if (frequency[i] != 0) frequency[i] = 65.41f * Mathf.Pow(1.05946f, frequency[i]);
        }
    }
}
