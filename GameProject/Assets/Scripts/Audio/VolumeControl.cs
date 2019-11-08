using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    Slider slider;
    AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        slider=GetComponent<Slider>();
        audioSources=FindObjectsOfType<AudioSource>();
        UpdateVolume();
    }

    // Update is called once per frame
    public void UpdateVolume()
    {
        foreach(AudioSource a in audioSources) a.volume=slider.value;   

    }

    public void ToggleMute()
    {
       foreach(AudioSource a in audioSources) a.mute=!a.mute;
    }

}
