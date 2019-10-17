using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SythEditor : EditorWindow
{
    public int ToolbarInt;

    public List<int> Track = new List<int>(1);

    public float WhiteKeyMin = 10, WhiteKeyMax = 50, BlackKeyMin = 5, BlackKeyMax = 25;

    public Rect DeselectWindow;    // Used to hold the points that are the sixe of the editor window

    [MenuItem("Tools/Synthesiser", priority = 21)]
    public static void ShowWindow()
    {
        GetWindow<SythEditor>("Synthesiser");
    }

    // Makes sure a new tack always has a first value
    private void Awake()
    {
        if (Track.Count == 0)
        {
            Track.Add(0);
        }
    }

    // What is shown
    private void OnGUI()
    {
        // Sets up the Deselect window to be the full size of the editor window
        DeselectWindow = new Rect(0, 0, position.width, position.height);

        EditorGUILayout.HelpBox("Syth: This is designed to create sounds to be played in game without taking up massive file size.", MessageType.Info);


        ToolbarInt = GUILayout.Toolbar(ToolbarInt, new string [] { "Manual Entry", "Piano" });

        switch (ToolbarInt)
        {
            case 0:
                EditorGUILayout.HelpBox("Syth: Manual Entry, this would be a version where the user would manually input either a string value of the note or a int value for the frequency. Not Pretty but it would work fine.", MessageType.Info);

                EditorGUILayout.BeginVertical();

                if (Track.Count == 0)
                {
                    Track.Add(0);
                }

                // The grouping on int fields, + & - buttons for each row
                for (int i = 0; i < Track.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(10), GUILayout.MaxWidth(15));

                    Track[i] = EditorGUILayout.IntField(Track[i]);

                    GUI.color = Color.green;
                    if (GUILayout.Button("+", GUILayout.MinWidth(20), GUILayout.MaxWidth(25)))
                    {
                        Track.Insert((i + 1), 0);
                    }
                    GUI.color = Color.red;
                    if (GUILayout.Button("-", GUILayout.MinWidth(20), GUILayout.MaxWidth(25)))
                    {
                        if (i == 0)
                        {
                            Track[i] = 0;
                        }
                        else
                        {
                            Track.RemoveAt(i);
                        }
                    }
                    GUI.color = Color.white;
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndVertical();
                break;
            case 1:
                EditorGUILayout.HelpBox("Syth: Piano Entry, This would let the user press the notes in order on a virtual keyboard, having an output the user could then edit in theory.", MessageType.Info);

                // Piano Option
                EditorGUILayout.BeginHorizontal();

                GUI.color = Color.white;
                // C - 1 / B# - 1
                if (GUILayout.Button("C - 1", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                // C# / Db - 1
                if (GUILayout.Button("", GUILayout.MinHeight(BlackKeyMin), GUILayout.MaxWidth(BlackKeyMax), GUILayout.Height(75)))
                {

                }

                GUI.color = Color.white;
                // D - 1
                if (GUILayout.Button("D", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                // D# / Eb - 1
                if (GUILayout.Button("", GUILayout.MinHeight(BlackKeyMin), GUILayout.MaxWidth(BlackKeyMax), GUILayout.Height(75)))
                {

                }


                GUI.color = Color.white;
                // E / Fb - 1
                if (GUILayout.Button("E", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;


                GUI.color = Color.white;
                // F / E# - 1
                if (GUILayout.Button("F", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                // F# / Gb - 1
                if (GUILayout.Button("", GUILayout.MinHeight(BlackKeyMin), GUILayout.MaxWidth(BlackKeyMax), GUILayout.Height(75)))
                {

                }

                GUI.color = Color.white;
                // G - 1
                if (GUILayout.Button("G", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                // G# / Ab - 1
                if (GUILayout.Button("", GUILayout.MinHeight(BlackKeyMin), GUILayout.MaxWidth(BlackKeyMax), GUILayout.Height(75)))
                {

                }

                GUI.color = Color.white;
                // A - 1
                if (GUILayout.Button("A", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                // A# / Bb - 1
                if (GUILayout.Button("", GUILayout.MinHeight(BlackKeyMin), GUILayout.MaxWidth(BlackKeyMax), GUILayout.Height(75)))
                {

                }

                GUI.color = Color.white;
                // B / Cb - 1
                if (GUILayout.Button("B", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }

                GUI.color = Color.white;
                // Middle C / B#
                if (GUILayout.Button("Mid C", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                // C# / Db
                if (GUILayout.Button("", GUILayout.MinHeight(BlackKeyMin), GUILayout.MaxWidth(BlackKeyMax), GUILayout.Height(75)))
                {

                }


                GUI.color = Color.white;
                // D
                if (GUILayout.Button("D", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                // D# / Eb
                if (GUILayout.Button("", GUILayout.MinHeight(BlackKeyMin), GUILayout.MaxWidth(BlackKeyMax), GUILayout.Height(75)))
                {

                }

                GUI.color = Color.white;
                // E / Fb
                if (GUILayout.Button("E", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                GUI.color = Color.white;
                // E# / F
                if (GUILayout.Button("F", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                // F# / Gb
                if (GUILayout.Button("", GUILayout.MinHeight(BlackKeyMin), GUILayout.MaxWidth(BlackKeyMax), GUILayout.Height(75)))
                {

                }

                GUI.color = Color.white;
                // G
                if (GUILayout.Button("G", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                // G# / Ab
                if (GUILayout.Button("", GUILayout.MinHeight(BlackKeyMin), GUILayout.MaxWidth(BlackKeyMax), GUILayout.Height(75)))
                {

                }

                GUI.color = Color.white;
                // A
                if (GUILayout.Button("A", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }
                GUI.color = Color.black;

                // A# / Bb
                if (GUILayout.Button("", GUILayout.MinHeight(BlackKeyMin), GUILayout.MaxWidth(BlackKeyMax), GUILayout.Height(75)))
                {

                }


                GUI.color = Color.white;
                // B / C#
                if (GUILayout.Button("B", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }

                // C + 1
                if (GUILayout.Button("C + 1", GUILayout.MinHeight(WhiteKeyMin), GUILayout.MaxWidth(WhiteKeyMax), GUILayout.Height(WhiteKeyMax * 2.5f)))
                {

                }

                EditorGUILayout.EndHorizontal();
                break;
            default:
                break;
        }


        // Makes it so you can deselect elements in the window by adding a button the size of the window that you can't see under everything
        //make sure the following code is at the very end of OnGUI Function
        if (GUI.Button(DeselectWindow, "", GUIStyle.none))
        {
            GUI.FocusControl(null);
        }
    }
}
