using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playsoundthingy : MonoBehaviour
{
    public string ClipSound;
    private SoundPlayer SP;

    // Start is called before the first frame update
    void Start()
    {
        SP = FindObjectOfType<SoundPlayer>();
        SP.PlayClip(ClipSound, .25f, true);
    }
}
