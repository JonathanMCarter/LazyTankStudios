using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShaderEditorGUI : ShaderGUI
{
    public Material Mat;


    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sprite Colours:", GUILayout.MaxWidth(100));
        properties[1].colorValue = EditorGUILayout.ColorField(properties[1].colorValue);
        properties[2].colorValue = EditorGUILayout.ColorField(properties[2].colorValue);
        properties[3].colorValue = EditorGUILayout.ColorField(properties[3].colorValue);
        properties[5].colorValue = EditorGUILayout.ColorField(properties[5].colorValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Palette Colours:", GUILayout.MaxWidth(100));
        properties[6].colorValue = EditorGUILayout.ColorField(properties[6].colorValue);
        properties[7].colorValue = EditorGUILayout.ColorField(properties[7].colorValue);
        properties[8].colorValue = EditorGUILayout.ColorField(properties[8].colorValue);
        properties[9].colorValue = EditorGUILayout.ColorField(properties[9].colorValue);
        EditorGUILayout.EndHorizontal();


        if (GUILayout.Button("Make an instance"))
        {
            Debug.Log(materialEditor.IsInstancingEnabled());
            Debug.Log(materialEditor.GetInstanceID());
        }

        base.OnGUI(materialEditor, properties);
    }
}
