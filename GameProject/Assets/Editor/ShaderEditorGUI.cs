using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Shader Editor Extra's
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 12/10/19 - added labels for help users understand the tool

    Edit History:
    - 6/10/19 - To add this comment bit in (nothing else was changed)

    This script adds the colour fields to the material dropdown, so you can edit the colours without needing an image palette

    NOTE: The create instance button which is commented out does nothing currently, I haven't figure out how to make instances on demand yet

*/

public class ShaderEditorGUI : ShaderGUI
{
    public enum Pallettes
    {
        Pallette1,
        Pallette2,
        Pallette3,
        Pallette4,
    };

    public Pallettes Pal;


    public bool EditSelection;

    private Material OldMat;
    public Material Mat;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        if (Mat == null)
        {
            OldMat = Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial;
        }

        Mat = Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial;

        EditSelection = EditorGUILayout.Toggle("Show Selection", EditSelection);

        if (EditSelection)
        {
            GUILayout.Space(10);
            EditorGUILayout.HelpBox("Here you can edit the selection. \nThis shouldn't need editing if the art has been imported right.", MessageType.None);
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Sprite Colours:", GUILayout.MaxWidth(100));
            Mat.SetColor("_TexCol1", EditorGUILayout.ColorField(properties[1].colorValue));
            Mat.SetColor("_TexCol2", EditorGUILayout.ColorField(properties[2].colorValue));
            Mat.SetColor("_TexCol3", EditorGUILayout.ColorField(properties[3].colorValue));
            Mat.SetColor("_TexCol4", EditorGUILayout.ColorField(properties[4].colorValue));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.LabelField("Edit the colours currently selected to the new colour.", EditorStyles.boldLabel);



        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Palette Colours:", GUILayout.MaxWidth(100));
        Mat.SetColor("_PalCol1", EditorGUILayout.ColorField(properties[5].colorValue));
        Mat.SetColor("_PalCol2", EditorGUILayout.ColorField(properties[6].colorValue));
        Mat.SetColor("_PalCol3", EditorGUILayout.ColorField(properties[7].colorValue));
        Mat.SetColor("_PalCol4", EditorGUILayout.ColorField(properties[8].colorValue));
        EditorGUILayout.EndHorizontal();

        // Show Buttons with colours on them
        //EditorGUILayout.BeginHorizontal();

        //GUI.color = new Color(properties[5].colorValue.r, properties[5].colorValue.g, properties[5].colorValue.b);
        //if (GUILayout.Button("", GUILayout.MaxWidth(25), GUILayout.MaxHeight(25)))
        //{

        //}

        //GUI.color = new Color(properties[6].colorValue.r, properties[6].colorValue.g, properties[6].colorValue.b);
        //if (GUILayout.Button("", GUILayout.MaxWidth(25), GUILayout.MaxHeight(25)))
        //{

        //}

        //EditorGUILayout.EndHorizontal();
        //EditorGUILayout.BeginHorizontal();

        //GUI.color = new Color(properties[7].colorValue.r, properties[7].colorValue.g, properties[7].colorValue.b);
        //if (GUILayout.Button("", GUILayout.MaxWidth(25), GUILayout.MaxHeight(25)))
        //{

        //}

        //GUI.color = new Color(properties[8].colorValue.r, properties[8].colorValue.g, properties[8].colorValue.b);
        //if (GUILayout.Button("", GUILayout.MaxWidth(25), GUILayout.MaxHeight(25)))
        //{

        //}
        //GUI.color = Color.white;

        //EditorGUILayout.EndHorizontal();


        if (GUILayout.Button("Make Material Instance"))
        {
            Selection.gameObjects[0].GetComponent<Renderer>().material = Selection.gameObjects[0].GetComponent<Renderer>().material;
        }

        // This doesn't work yet
        //if (GUILayout.Button("Reset Instance"))
        //{
        //    Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial = OldMat;
        //}

        //if (GUILayout.Button("Make an instance"))
        //{
        //    Debug.Log(materialEditor.IsInstancingEnabled());
        //    Debug.Log(materialEditor.GetInstanceID());
        //}

        base.OnGUI(materialEditor, properties);
    }
}
