using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDataGather : MonoBehaviour
{
    public AudioClip TheMusic;
    public AudioClip NewMusic;
    public float[] Data;
    // Start is called before the first frame update
    void Start()
    {

        //Data = new float[TheMusic.samples *  2];
        //TheMusic.GetData(Data, 0);
        NewMusic = AudioClip.Create("CombinedClip", Data.Length / 2, 2, 44100, false);
        NewMusic.SetData(Data, 0);
        GetComponent<AudioSource>().clip = NewMusic;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
