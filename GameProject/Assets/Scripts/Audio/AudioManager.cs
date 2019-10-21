using UnityEngine.Audio;
using UnityEngine;

/*
 * Audio Manager
 * 
 *  Controls the Playback of Soundeffects 
 *
 * Owner: Andreas Kraemer
 * Last Edit : 6/10/19
 * 
 * Also Edited by : <Enter name here if you edit this script>
 * Last Edit: <Date here if you edit this script>
 * 
 * */

public class AudioManager : MonoBehaviour
{
    //Insert sound file into the array via the editor, give it a name and set volume and pitch
    public Sound[] sounds;
    ///<summary>
    ///Call Play([insert name of soundeffect here]) to play a sound
    ///</summary>
    public void Play(string name)
    {
        foreach(Sound s in sounds)
        {
            if (s.name.Equals(name))
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.audioClip;
                s.source.Play();
            }
        }
    }
}
