using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// made by jonathan to be used to play sounds from syth, also a good example of how to use it for those who need it.
public class PlaySythSound : MonoBehaviour
{
    public string ClipSound;
    private SoundPlayer SP;

    // Start is called before the first frame update
    void Start()
    {
        SP = GetComponentInChildren<SoundPlayer>();
        SP.PlayClip(ClipSound, true);
    }
}
