using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Quest File", menuName = "Carter Games/Quest File")]
public class QuestItem : ScriptableObject
{
    public List<string> TaskText;
}