using UnityEngine;
public class PlaySythSound : MonoBehaviour
{
    public string ClipSound;
    private SoundPlayer SP;
    void Start()
    {
        SP = GetComponent<SoundPlayer>();
        SP.Play(ClipSound, true);
    }
}
