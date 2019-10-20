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
    private Material OldMat;
    public Material Mat;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        if (Mat == null)
        {
            OldMat = Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial;
        }

        Mat = Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial;

        EditorGUILayout.LabelField("Select the colours currently on the sprite that you want to change.", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sprite Colours:", GUILayout.MaxWidth(100));
        Mat.SetColor("_TexCol1", EditorGUILayout.ColorField(properties[1].colorValue));
        Mat.SetColor("_TexCol2", EditorGUILayout.ColorField(properties[2].colorValue));
        Mat.SetColor("_TexCol3", EditorGUILayout.ColorField(properties[3].colorValue));
        Mat.SetColor("_TexCol4", EditorGUILayout.ColorField(properties[4].colorValue));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Edit the colours currently selected to the new colour.", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Palette Colours:", GUILayout.MaxWidth(100));
        Mat.SetColor("_PalCol1", EditorGUILayout.ColorField(properties[5].colorValue));
        Mat.SetColor("_PalCol2", EditorGUILayout.ColorField(properties[6].colorValue));
        Mat.SetColor("_PalCol3", EditorGUILayout.ColorField(properties[7].colorValue));
        Mat.SetColor("_PalCol4", EditorGUILayout.ColorField(properties[8].colorValue));
        EditorGUILayout.EndHorizontal();



        if (GUILayout.Button("Make Material Instance"))
        {
            Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial = Material.Instantiate<Material>(Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial);
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
