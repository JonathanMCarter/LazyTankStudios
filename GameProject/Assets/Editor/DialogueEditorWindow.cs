using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DialogueEditorWindow : EditorWindow
{
    public DialogueFile CurrentFile;
    private bool ShowFile;
    private int Tab;

    private bool FileCreated;
    private bool CreateFile;

    private string FileName;
    private string TextInputted;
    private int NumberOfLines = 1;
    public List<string> NewNames = new List<string>();
    public List<string> NewDialogue = new List<string>();

    private Color32 AddBtnColour = new Color32(55, 175, 95, 255);
    public Color32 RemoveBtnColour;

    public Rect DeselectWindow;
    Vector2 ScrollPos;
    //GUIStyle Style;


    [MenuItem("Tools/DialogueEditor")]
    public static void ShowWindow()
    {
        GetWindow<DialogueEditorWindow>("Dialogue Editor");
    }


    // The stuff that shows up in the window
    public void OnGUI()
    {
        DeselectWindow = new Rect(0, 0, position.width, position.height);

        GUILayout.Space(15f);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        Tab = GUILayout.Toolbar(Tab, new string[] { "Create New File", "Edit Existing File" }, GUILayout.MaxWidth(250f), GUILayout.MaxHeight(25f));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5f);

        NewNames.Add("");
        NewDialogue.Add("");

        switch (Tab)
        {
            // If Creating a new file
            case 0:

                GUILayout.Space(20);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("File Name:", GUILayout.MaxWidth(65f));
                FileName = EditorGUILayout.TextField(FileName);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(10f);

                // Displays the table headers
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("No:", GUILayout.MaxWidth(30f));
                EditorGUILayout.LabelField("Character:", GUILayout.MaxWidth(200f));
                EditorGUILayout.LabelField("Dialogue:");
                EditorGUILayout.EndHorizontal();

                ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.Width(position.width), GUILayout.ExpandHeight(true));

                // Displays the table for the number of lines added to the file
                for (int i = 0; i < NumberOfLines; i++)
                {
                    Debug.Log("for loop running");

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(30f));
                    NewNames[i] = EditorGUILayout.TextField(NewNames[i], GUILayout.MaxWidth(200f));
                    NewDialogue[i] = EditorGUILayout.TextField(NewDialogue[i]);

                    GUILayout.Space(10f);

                    GUI.backgroundColor = Color.green;
                    if (GUILayout.Button("+", GUILayout.Width(25)))
                    {
                        NumberOfLines++;
                        NewNames.Insert(i + 1, "");
                        NewDialogue.Insert(i + 1, "");
                    }
                    GUI.backgroundColor = Color.red;
                    if (GUILayout.Button("-", GUILayout.Width(25)))
                    {
                        if (i != 0)
                        {
                            NumberOfLines--;
                            NewNames.RemoveAt(i);
                            NewDialogue.RemoveAt(i);
                        }
                    }
                    GUI.backgroundColor = Color.white;
                    EditorGUILayout.EndHorizontal();
                }

                // Creates a new Dialogue File with the name and text inputted
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Create File", GUILayout.MaxWidth(100f)))
                {
                    CreateFile = true;
                }
                GUI.backgroundColor = Color.white;
                if (GUILayout.Button("Reset File", GUILayout.MaxWidth(100f)))
                {
                    ClearLists();
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndScrollView();

                break;

            // If editing an exsisting file
            case 1:



                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Here you can edit already exsisting files as if you were creating them.");
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(10);

                // Label & Box for the user to select the Dialogue FIle they wish to edit
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("Dialogue File: ", GUILayout.MaxWidth(80f));
                CurrentFile = (DialogueFile)EditorGUILayout.ObjectField(CurrentFile, typeof(DialogueFile), false);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();

                if (CurrentFile != null)
                {
                    GUILayout.Space(20);

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.LabelField("Edit File Name:", GUILayout.MaxWidth(100f));
                    CurrentFile.name = EditorGUILayout.TextField(CurrentFile.name);
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(10);
                    EditorGUILayout.LabelField("Contents of File:", GUILayout.MaxWidth(100f));
                    GUILayout.Space(5);

                    // Table Headers
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("No:", GUILayout.MaxWidth(30f));
                    EditorGUILayout.LabelField("Character:", GUILayout.MaxWidth(200f));
                    EditorGUILayout.LabelField("Dialogue:");
                    EditorGUILayout.EndHorizontal();

                    ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.Width(position.width), GUILayout.ExpandHeight(true));

                    // Displays the current file's dialogue in the correct boxes and allow it to be edited
                    for (int i = 0; i < CurrentFile.Names.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(30f));
                        CurrentFile.Names[i] = EditorGUILayout.TextField(CurrentFile.Names[i], GUILayout.MaxWidth(200f));
                        CurrentFile.Dialogue[i] = EditorGUILayout.TextField(CurrentFile.Dialogue[i]);
                        GUILayout.Space(10);

                        GUI.backgroundColor = Color.green;
                        if (GUILayout.Button("+", GUILayout.Width(25)))
                        {
                            NumberOfLines++;
                            CurrentFile.Names.Insert(i + 1, "");
                            CurrentFile.Dialogue.Insert(i + 1, "");

                            Debug.Log("Adding new line");
                        }
                        GUI.backgroundColor = Color.red;
                        if (GUILayout.Button("-", GUILayout.Width(25)))
                        {
                            if (i != 0)
                            {
                                NumberOfLines--;
                                CurrentFile.Names.RemoveAt(i);
                                CurrentFile.Dialogue.RemoveAt(i);
                            }
                        }
                        GUI.backgroundColor = Color.white;
                        EditorGUILayout.EndHorizontal();
                    }
                }
                else
                { EditorGUILayout.LabelField("No Dialogue File Selected!"); }
                EditorGUILayout.EndScrollView();
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


    private void Update()
    {
        if (CreateFile)
        {
            CreateNewFile();
            CreateFile = false;
        }
    }


    private void CreateNewFile()
    {
        DialogueFile asset = ScriptableObject.CreateInstance<DialogueFile>();

        asset.Names = new List<string>();
        asset.Dialogue = new List<string>();

        for (int i = 0; i < NumberOfLines; i++)
        {
            asset.Names.Add(NewNames[i]);
            asset.Dialogue.Add(NewDialogue[i]);
        }

        AssetDatabase.CreateAsset(asset, "Assets/Dialogue/" + FileName + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();

        ClearLists();
    }


    private void ClearLists()
    {
        if (FileName != null)
        {
            FileName = "";
        }

        NewNames.Clear();
        NewDialogue.Clear();
        NumberOfLines = 1;
        NewNames.Add("");
        NewDialogue.Add("");
    }

    // Old Function : Checked to see if the window size was too small to display stuff correctly, however as I got a scroll view working this isn't needed
    //private bool CheckSize()
    //{
    //    if (position.width < 400f)
    //    {
    //        EditorGUILayout.LabelField("Window is too small to display... please make the window bigger");
    //        return false;
    //    }
    //    else if (position.height < 150f)
    //    {
    //        EditorGUILayout.LabelField("Window is too small to display... please make the window bigger");
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
}
