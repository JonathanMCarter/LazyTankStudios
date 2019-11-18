using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue File", menuName = "Carter Games/Dialogue File")]
public class DialogueFile : ScriptableObject
{
    public List<string> Names;
    public List<string> Dialogue;
}
