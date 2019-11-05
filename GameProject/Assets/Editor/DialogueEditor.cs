using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Dialouge Editor Script
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 15/10/19 - Removed old bits of the editor script

    Edit History:
    - 6/10/19 - To add this comment bit in (nothing else was changed)

    This script alteres the inspector for the DialogueScript with a load of custom fields before the origional inspector is shown underneath


    NOTE: This was made over summer for an asset store project that I've yet to finish, so it has branding as if I was to release it in the script, just ignore this stuff...

*/

[CustomEditor(typeof(DialogueScript))]
public class DialogueEditor : Editor
{
    private DialogueScript Script;


    // Overrides the Inspecotr GUI for the Dialogue Script
    public override void OnInspectorGUI()
	{
        var Script = target as DialogueScript;

        //EditorGUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //// Carter Games Logo
        //if (GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Logo"), GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
        //{
        //    GUI.FocusControl(null);
        //}
        //GUILayout.FlexibleSpace();
        //EditorGUILayout.EndHorizontal();


        //EditorGUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //EditorGUILayout.LabelField("Dialogue Manager | V: 1.0");
        //GUILayout.FlexibleSpace();
        //EditorGUILayout.EndHorizontal();

        GUILayout.Space(20);

		EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

		if (GUILayout.Button("Open File Editor"))
		{
            DialogueEditorWindow.ShowWindow();
        }

		//if (GUILayout.Button("Documentation"))
		//{
  //          //Application.OpenURL("");
		//}

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


        // Base inspector - Disabled as this isn't used really.
        base.OnInspectorGUI();
	}
}
