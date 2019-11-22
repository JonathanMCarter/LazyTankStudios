using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioDataGather: A {
 public AudioClip TheMusic;
 public AudioClip NewMusic;
 public float[] Data;
 void Start() {
  NewMusic = AudioClip.Create("CombinedClip", Data.Length / 2, 2, 44100, false);
  NewMusic.SetData(Data, 0);
  G<AudioSource> ().clip = NewMusic;
  G<AudioSource> ().Play();
 }
}