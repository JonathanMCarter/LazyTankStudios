using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventDrawer : Editor
{
    private GameEvent gameEvent;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if(GUILayout.Button("Raise Event"))
            ((GameEvent)target).Raise();
    }
}
