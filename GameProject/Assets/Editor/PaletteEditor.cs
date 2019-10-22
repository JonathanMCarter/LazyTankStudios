using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class PaletteEditor : EditorWindow
{

    private bool HasInit;

    public List<Color> Colours = new List<Color>();
    public bool AreDisplayed = false;
    public static int NumberofColours = 12;

    [MenuItem("Tools/Palette Editor", priority = 22)]
    public static void ShowWindow()
    {
        GetWindow<PaletteEditor>("Palette Editor Window");
    }


    private void Awake()
    {
        AreDisplayed = false;

        for (int i = 0; i < 12; i++)
        {
            Colours.Add(new Color());
        }
    }


    public void OnGUI()
    {
        EditorGUILayout.LabelField("fhdjkshdjkfhdjks");


        if (!AreDisplayed)
        {
            for (int i = 0; i < 12; i++)
            {
                EditorGUILayout.ColorField(Colours[i]);
            }
        }
    }
}
