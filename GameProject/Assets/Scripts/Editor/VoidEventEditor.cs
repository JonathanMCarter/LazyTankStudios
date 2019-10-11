using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VoidEvent))]
public class VoidEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        VoidEvent e = (VoidEvent)target;

        if (GUILayout.Button("Raise"))e.Raise();
    }
}