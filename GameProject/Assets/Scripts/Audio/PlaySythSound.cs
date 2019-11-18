using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlaySythSound : MonoBehaviour
{
    public string ClipSound;
    private SoundPlayer SP;
    void Start()
    {
        SP = GetComponentInChildren<SoundPlayer>();
    }
}
