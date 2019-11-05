using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// Changes the Shader Editor so it is more user friendly 

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Shader Editor Extra's
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 03/11/19 - Added useage of Perm Palette instead of the stores colours in the shader

    Edit History:
    - 03/11/19 - Fixed problem where palette's revert when you select the object in the inspector
    - 28/10/19 - Added option to edit 4th colour in palette as well as instance again so not to effect all versions of the material
    - 27/10/19 - added 4 palette restriction
    - 12/10/19 - added labels for help users understand the tool
    - 6/10/19 - To add this comment bit in (nothing else was changed)

    This script adds the colour fields to the material dropdown, so you can edit the colours without needing an image palette

    NOTE: The create instance button which is commented out does nothing currently, I haven't figure out how to make instances on demand yet

*/


/// Editor script for the coliur changer shader. This script alters the inspector appearance of the shader values so that it is easier for the user to understand.
public class ShaderEditorGUI : ShaderGUI
{
    /// Definition An enum for the Palettes the user can choose between
    public enum Palettes
    {
        Palette1,
        Palette2,
        Palette3,
        Palette4,
    };

    /// Variable variable that the user edits 
    public Palettes Pal;

    /// Palette The main and only palette that is needed for the game, it hold 4 lists of 3 colours that are used in the shader.
    public Palette PermColours = (Palette)AssetDatabase.LoadAssetAtPath("Assets/Palette/Palettes.asset", typeof(Palette));

    /// Boolean that controls whether or not the material is instanced. This inturn controls whether or not the user has acced to edit the 4th colour in each palette.
    private bool IsTrans;

    /// Controls whether or not the user has access to the selected colours, these are the colours that are set to the greyscale values by default.
    public bool EditSelection;
    /// This string holds the value shown in the button on the editor, it just updates based on whther or not the EditSelection bool is true or false.
    public string EditSelectionString;

    /// Holds the material for the gameobject it is on.
    public Material Mat;

    /// Overrides the default GUI to show the custom inspector, param are passed in by default from the ShaderGUI editor type
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        PermColours = (Palette)AssetDatabase.LoadAssetAtPath("Assets/Palette/Palettes.asset", typeof(Palette));
        Mat = Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial;

        ChangeString();
        SetPalette();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(EditSelectionString))
        {
            EditSelection = !EditSelection;
        }

        // This is broken currently, don't know why atm, still working on this...
        //if (GUILayout.Button("Open Editor"))
        //{
        //    PaletteEditor Test = EditorWindow.GetWindow<PaletteEditor>();
        //    Test.Version = this;
        //}

        if (Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial.GetFloat("_IsInstance") == 0)
        {
            if (GUILayout.Button("Make Instance"))
            {
                Selection.gameObjects[0].GetComponent<Renderer>().material = Selection.gameObjects[0].GetComponent<Renderer>().material;
                Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial.SetFloat("_IsInstance", 1);
            }
        }
        else
        {
            if (GUILayout.Button("Revert Instance"))
            {
                var Test = new MaterialPropertyBlock();
                Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial.name = "ColourChanger_MAT";
                Selection.gameObjects[0].GetComponent<Renderer>().GetPropertyBlock(Test);
                Selection.gameObjects[0].GetComponent<Renderer>().sharedMaterial.SetFloat("_IsInstance", 0);
            }
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
                GUI.color = PermColours.Pal1[0];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = PermColours.Pal1[1];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = PermColours.Pal1[2];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                if ((Mat.GetFloat("_IsInstance") > 0) && (IsTrans))
                {
                    GUI.color = Mat.GetColor("_StoreTrans1");
                    GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                Mat.SetColor("_PalCol1", PermColours.Pal1[0]);
                Mat.SetColor("_PalCol2", PermColours.Pal1[1]);
                Mat.SetColor("_PalCol3", PermColours.Pal1[2]);


                if ((Mat.GetFloat("_IsInstance") > 0) && (IsTrans))
                {
                    Mat.SetColor("_PalCol4", Mat.GetColor("_StoreTrans1"));
                }
                else
                {
                    Mat.SetColor("_PalCol4", Color.clear);
                }

                if (Mat.GetFloat("_PaletteSelected") != (int)Pal + 1)
                {
                    Mat.SetFloat("_PaletteSelected", (int)Pal + 1);
                }

                    break;
            case Palettes.Palette2:
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUI.color = PermColours.Pal2[0];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = PermColours.Pal2[1];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = PermColours.Pal2[2];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                if ((Mat.GetFloat("_IsInstance") > 0) && (IsTrans))
                {
                    GUI.color = Mat.GetColor("_StoreTrans2");
                    GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                Mat.SetColor("_PalCol1", PermColours.Pal2[0]);
                Mat.SetColor("_PalCol2", PermColours.Pal2[1]);
                Mat.SetColor("_PalCol3", PermColours.Pal2[2]);

                if ((Mat.GetFloat("_IsInstance") > 0) && (IsTrans))
                {
                    Mat.SetColor("_PalCol4", Mat.GetColor("_StoreTrans2"));
                }
                else
                {
                    Mat.SetColor("_PalCol4", Color.clear);
                }

                if (Mat.GetFloat("_PaletteSelected") != (int)Pal + 1)
                {
                    Mat.SetFloat("_PaletteSelected", (int)Pal + 1);
                }

                break;
            case Palettes.Palette3:
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUI.color = PermColours.Pal3[0];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = PermColours.Pal3[1];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = PermColours.Pal3[2];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                if ((Mat.GetFloat("_IsInstance") > 0) && (IsTrans))
                {
                    GUI.color = Mat.GetColor("_StoreTrans3");
                    GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                Mat.SetColor("_PalCol1", PermColours.Pal3[0]);
                Mat.SetColor("_PalCol2", PermColours.Pal3[1]);
                Mat.SetColor("_PalCol3", PermColours.Pal3[2]);

                if ((Mat.GetFloat("_IsInstance") > 0) && (IsTrans))
                {
                    Mat.SetColor("_PalCol4", Mat.GetColor("_StoreTrans3"));
                }
                else
                {
                    Mat.SetColor("_PalCol4", Color.clear);
                }

                if (Mat.GetFloat("_PaletteSelected") != (int)Pal + 1)
                {
                    Mat.SetFloat("_PaletteSelected", (int)Pal + 1);
                }

                break;
            case Palettes.Palette4:
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUI.color = PermColours.Pal4[0];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = PermColours.Pal4[1];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                GUI.color = PermColours.Pal4[2];
                GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                if ((Mat.GetFloat("_IsInstance") > 0) && (IsTrans))
                {
                    GUI.color = Mat.GetColor("_StoreTrans4");
                    GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50));
                }
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                Mat.SetColor("_PalCol1", PermColours.Pal4[0]);
                Mat.SetColor("_PalCol2", PermColours.Pal4[1]);
                Mat.SetColor("_PalCol3", PermColours.Pal4[2]);

                if ((Mat.GetFloat("_IsInstance") > 0) && (IsTrans))
                {
                    Mat.SetColor("_PalCol4", Mat.GetColor("_StoreTrans4"));
                }
                else
                {
                    Mat.SetColor("_PalCol4", Color.clear);
                }

                if (Mat.GetFloat("_PaletteSelected") != (int)Pal + 1)
                {
                    Mat.SetFloat("_PaletteSelected", (int)Pal + 1);
                }

                break;
            default:
                break;
        }


        GUI.color = Color.white;

        if (Mat.GetFloat("_IsInstance") > 0)
        { 
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use 4th Colour?", GUILayout.Width(95));
            IsTrans = EditorGUILayout.Toggle(IsTrans);
            EditorGUILayout.EndHorizontal();

            if ((IsTrans) && (Mat.GetFloat("_UseTrans") < 1)) { Mat.SetFloat("_UseTrans", 0); }
            else { Mat.SetFloat("_UseTrans", 1); }
        }



        //base.OnGUI(materialEditor, properties);
    }

    /// Edits the string value for the colour selection button so it updates when the button is pressed.
    private void ChangeString()
    {
        if (EditSelection) { EditSelectionString = "Hide Selection"; }
        else { EditSelectionString = "Show Selection"; }
    }


    // Sets the palette to the right palette when selected - simple problem as the enum is reset, so I've added a float to the material which saves said info
    private void SetPalette()
    {
        int Palette = (int)Mat.GetFloat("_PaletteSelected");

        switch (Palette)
        {
            case 1:
                Pal = Palettes.Palette1;
                break;
            case 2:
                Pal = Palettes.Palette2;
                break;
            case 3:
                Pal = Palettes.Palette3;
                break;
            case 4:
                Pal = Palettes.Palette4;
                break;
            default:
                break;
        }
    }
}