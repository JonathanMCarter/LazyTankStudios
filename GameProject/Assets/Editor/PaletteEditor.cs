using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    public ShaderEditorGUI Version;

    public Palette PermColours;

    private int ToolBarValue;

    public void OnGUI()
    {
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


                    //Version.Mat.SetColor("_StoreCol1", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol1")));
                    //Version.Mat.SetColor("_StoreCol2", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol2")));
                    //Version.Mat.SetColor("_StoreCol3", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol3")));

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

                    //Version.Mat.SetColor("_StoreCol4", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol4")));
                    //Version.Mat.SetColor("_StoreCol5", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol5")));
                    //Version.Mat.SetColor("_StoreCol6", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol6")));

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

                    //Version.Mat.SetColor("_StoreCol7", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol7")));
                    //Version.Mat.SetColor("_StoreCol8", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol8")));
                    //Version.Mat.SetColor("_StoreCol9", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol9")));

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

                    //Version.Mat.SetColor("_StoreCol10", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol10")));
                    //Version.Mat.SetColor("_StoreCol11", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol11")));
                    //Version.Mat.SetColor("_StoreCol12", EditorGUILayout.ColorField(Version.Mat.GetColor("_StoreCol12")));

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
