﻿public class MusicChanger : A
{
    public string newMusic;
    void Start()
    {
        FindObjectOfType<SoundPlayer>().PlayMusic(newMusic);        
    }
}
