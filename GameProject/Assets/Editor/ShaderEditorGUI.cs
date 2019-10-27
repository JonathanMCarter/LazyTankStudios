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
    Date Edited Last: 27/10/19 - added 4 palette restriction

    Edit History:
    - 12/10/19 - added labels for help users understand the tool
    - 6/10/19 - To add this comment bit in (nothing else was changed)

    This script adds the colour fields to the material dropdown, so you can edit the colours without needing an image palette

    NOTE: The create instance button which is commented out does nothing currently, I haven't figure out how to make instances on demand yet

*/

public class ShaderEditorGUI : ShaderGUI
{
    public enum Palettes
    {
        Palette1,
        Palette2,
        Palette3,
        Palette4,
    };

    public Palettes Pal;


    public bool EditSelection;
    public string EditSelectionString;

    private Material OldMat;
    public Material Mat;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        ChangeString();

        if (Mat == null)
        {
            OldMat = Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial;
        }

        Mat = Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial;


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(EditSelectionString))
        {
            EditSelection = !EditSelection;
        }

        if (GUILayout.Button("Open Editor"))
        {
            PaletteEditor Test = EditorWindow.GetWindow<PaletteEditor>();
            Test.Version = this;
        }
        EditorGUILayout.EndHorizontal();

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

        EditorGUILayout.LabelField("Select Palette to change to...", EditorStyles.boldLabel);

        Pal = (Palettes)EditorGUILayout.EnumPopup(Pal);


        switch (Pal)
        {
            case Palettes.Palette1:
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUI.color = Mat.GetColor("_StoreCol1");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = Mat.GetColor("_StoreCol2");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = Mat.GetColor("_StoreCol3");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                Mat.SetColor("_PalCol1", Mat.GetColor("_StoreCol1"));
                Mat.SetColor("_PalCol2", Mat.GetColor("_StoreCol2"));
                Mat.SetColor("_PalCol3", Mat.GetColor("_StoreCol3"));

                break;
            case Palettes.Palette2:
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUI.color = Mat.GetColor("_StoreCol4");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = Mat.GetColor("_StoreCol5");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = Mat.GetColor("_StoreCol6");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                Mat.SetColor("_PalCol1", Mat.GetColor("_StoreCol4"));
                Mat.SetColor("_PalCol2", Mat.GetColor("_StoreCol5"));
                Mat.SetColor("_PalCol3", Mat.GetColor("_StoreCol6"));
                break;
            case Palettes.Palette3:
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUI.color = Mat.GetColor("_StoreCol7");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = Mat.GetColor("_StoreCol8");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = Mat.GetColor("_StoreCol9");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                Mat.SetColor("_PalCol1", Mat.GetColor("_StoreCol7"));
                Mat.SetColor("_PalCol2", Mat.GetColor("_StoreCol8"));
                Mat.SetColor("_PalCol3", Mat.GetColor("_StoreCol9"));
                break;
            case Palettes.Palette4:
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUI.color = Mat.GetColor("_StoreCol10");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = Mat.GetColor("_StoreCol11");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = Mat.GetColor("_StoreCol12");
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                Mat.SetColor("_PalCol1", Mat.GetColor("_StoreCol10"));
                Mat.SetColor("_PalCol2", Mat.GetColor("_StoreCol11"));
                Mat.SetColor("_PalCol3", Mat.GetColor("_StoreCol12"));
                break;
            default:
                break;
        }

        GUI.color = Color.white;

        base.OnGUI(materialEditor, properties);
    }

    private void ChangeString()
    {
        if (EditSelection) { EditSelectionString = "Hide Selection"; }
        else { EditSelectionString = "Show Selection"; }
    }
}
