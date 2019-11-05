using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// This script adds an editor window which is opened by a button on the Shader Editor GUI inspector override.
/// This script does the majority of the work when it comes to changing the colours in the palette. 

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Palette Editor Window
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 03/11/19 - Added useage of Perm Palette instead of the stores colours in the shader

    History:
    - 03/11/19 - Fixed problem where palette's revert when you select the object in the inspector
    - 27/10/19 - finished window ready for use

    This script adds an editor window which is opened by a button on the Shader Editor GUI inspector override.

*/

public class PaletteEditor : EditorWindow
{
    /// This is used to hold a referance to the ShaderEditorGUI that called it, this is the only way I've found to refer to it. 
    /// @attention This has to be set otherwise it wil not display anything other than the tabs and will throw errors in the console!
    public ShaderEditorGUI Version;

    /// This holds the value for the Palette Scriptable Object, its used to allow you to change the colours.
    /// @note This should reference automatically, as long as the palettes object that it uses is not renamed / moved
    [SerializeField]
    public Palette PermColours; 

    // used for the tabs only
    private int ToolBarValue;


    /// The function the runs all the visuals for the editor, it runs like update but only when you touch that part of the editor.
    public void OnGUI()
    {
        PermColours = (Palette)AssetDatabase.LoadAssetAtPath("Assets/Palette/Palettes.asset", typeof(Palette));

        if (Version == null)
        {
            Close();
        }
        else
        {

            ToolBarValue = GUILayout.Toolbar(ToolBarValue, new string[] { "Palette 1", "Palette 2", "Palette 3", "Palette 4" });

            switch (ToolBarValue)
            {
                case 0:
                    EditorGUILayout.BeginHorizontal();
                    PermColours.Pal1[0] = EditorGUILayout.ColorField(PermColours.Pal1[0]);
                    PermColours.Pal1[1] = EditorGUILayout.ColorField(PermColours.Pal1[1]);
                    PermColours.Pal1[2] = EditorGUILayout.ColorField(PermColours.Pal1[2]);

                    if (Version.Mat.GetFloat("_IsInstance") > 0)
                    {
                        Version.Mat.SetColor("_StoreTrans1", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreTrans1")));
                    }

                    EditorGUILayout.EndHorizontal();
                    break;
                case 1:
                    EditorGUILayout.BeginHorizontal();
                    PermColours.Pal2[0] = EditorGUILayout.ColorField(PermColours.Pal2[0]);
                    PermColours.Pal2[1] = EditorGUILayout.ColorField(PermColours.Pal2[1]);
                    PermColours.Pal2[2] = EditorGUILayout.ColorField(PermColours.Pal2[2]);

                    if (Version.Mat.GetFloat("_IsInstance") > 0)
                    {
                        Version.Mat.SetColor("_StoreTrans2", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreTrans2")));
                    }

                    EditorGUILayout.EndHorizontal();
                    break;
                case 2:
                    EditorGUILayout.BeginHorizontal();
                    PermColours.Pal3[0] = EditorGUILayout.ColorField(PermColours.Pal3[0]);
                    PermColours.Pal3[1] = EditorGUILayout.ColorField(PermColours.Pal3[1]);
                    PermColours.Pal3[2] = EditorGUILayout.ColorField(PermColours.Pal3[2]);

                    if (Version.Mat.GetFloat("_IsInstance") > 0)
                    {
                        Version.Mat.SetColor("_StoreTrans3", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreTrans3")));
                    }

                    EditorGUILayout.EndHorizontal();
                    break;
                case 3:
                    EditorGUILayout.BeginHorizontal();
                    PermColours.Pal4[0] = EditorGUILayout.ColorField(PermColours.Pal4[0]);
                    PermColours.Pal4[1] = EditorGUILayout.ColorField(PermColours.Pal4[1]);
                    PermColours.Pal4[2] = EditorGUILayout.ColorField(PermColours.Pal4[2]);

                    if (Version.Mat.GetFloat("_IsInstance") > 0)
                    {
                        Version.Mat.SetColor("_StoreTrans4", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreTrans4")));
                    }

                    EditorGUILayout.EndHorizontal();
                    break;
                default:
                    break;
            }
        }
    }
}