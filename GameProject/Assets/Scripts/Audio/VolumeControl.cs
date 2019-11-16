using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : A
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider=GetComponent<Slider>();
        //audioSources=FindObjectsOfType<AudioSource>();
        UpdateVolume();
    }

    // Update is called once per frame
    public void UpdateVolume()
    {
        AudioListener.volume = slider.value;
        

    }

    public void ToggleMute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

}
