using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(custominspector))]
public class CustomInspector : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    custominspector inspector = (custominspector)target;
    //    SerializedProperty show = serializedObject.FindProperty("Items");
    //    EditorGUI.PropertyField(Rect.MinMaxRect(0, 0, 50, 50), show);
    //    serializedObject.ApplyModifiedProperties();

    //}
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
       
    }
}
