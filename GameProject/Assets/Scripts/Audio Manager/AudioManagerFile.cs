using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Manager File", menuName = "Carter Games/Audio Manager File")]
public class AudioManagerFile : ScriptableObject
{
    public List<string> ClipName;
    public List<AudioClip> Clip;
    public GameObject Prefab;
    public bool Populated;
}
