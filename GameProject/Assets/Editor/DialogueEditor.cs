using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(DialogueScript))]
public class DialogueEditor : Editor
{
    private DialogueScript Script;


    // Overrides the Inspecotr GUI for the Dialogue Script
    public override void OnInspectorGUI()
	{
        var Script = target as DialogueScript;

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        // Carter Games Logo
        if (GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Logo"), GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
        {
            GUI.FocusControl(null);
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Dialogue Manager | V: 1.0");
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

		if (GUILayout.Button("Open Editor"))
		{
            DialogueEditorWindow.ShowWindow();
        }

		if (GUILayout.Button("Documentation"))
		{
            //Application.OpenURL("");
		}
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("File in use: ", GUILayout.MaxWidth(65));
        Script.File = (DialogueFile)EditorGUILayout.ObjectField(Script.File, typeof(DialogueFile), false);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Character Name: ", GUILayout.MaxWidth(100));
        Script.DialName = (Text)EditorGUILayout.ObjectField(Script.DialName, typeof(Text), false);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Character Text: ", GUILayout.MaxWidth(100));
        Script.DialText = (Text)EditorGUILayout.ObjectField(Script.DialText, typeof(Text), false);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        EditorGUILayout.BeginHorizontal();
        Script.DisplayStyle = (Styles)EditorGUILayout.EnumPopup("Display Mode: ", Script.DisplayStyle);
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
	}
}
