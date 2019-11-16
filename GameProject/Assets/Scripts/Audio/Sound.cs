using UnityEngine.Audio;
using UnityEngine;

/// <summary>
/// /////////Not Needed
/// </summary>

[System.Serializable]
public class Sound 
{
    public AudioClip audioClip;

    public string name;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
