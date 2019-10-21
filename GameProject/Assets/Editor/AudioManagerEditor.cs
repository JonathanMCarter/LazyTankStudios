using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/*
 * 
 *							Audio Manager Editor Script
 *			
 *			Script written by: Jonathan Carter (https://jonathan.carter.games)
 *									Version: 2.1
 *							  Last Updated: 22/08/19						
 * 
 * 


[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
	// Colours for the Editor Buttons
	private Color32 ScanCol = new Color32(41, 176, 97, 255);
	private Color32 ScannedCol = new Color32(189, 191, 60, 255);
	private Color32 RedCol = new Color32(190, 42, 42, 255);
	private Color32 CyanCol = new Color32(100, 215, 175, 255);

	private bool SortedAudio;			// Bool for if the audio has been sorted
	private bool HasScannedOnce;		// Bool for if the scan button has been pressed before
	private string ScanButtonString;	// String for the value of the scan button text

	private List<AudioClip> AudioList;	// List of Audioclips used to add the audio to the library in the Audio Manager Script
	private List<string> AudioStrings;	// List of Strings used to add the names of the audio clips to the library in the Audio Manager Script

	private AudioManager Script;        // Reference to the Audio Manager Script that this script overrides the inspector for

	// When the script first enables - do this stuff!
	private void OnEnable()
	{
		// References the Audio Manager Script
		Script = target as AudioManager;		

		// Adds an Audio Source to the gameobject this script is on if its not already there (used for previewing audio only) 
		// * Hide flags hides it from the inspector so you don't notice it there *
		if (Script.gameObject.GetComponent<AudioSource>())
		{
			Script.gameObject.GetComponent<AudioSource>().hideFlags = HideFlags.HideInInspector;
			Script.GetComponent<AudioSource>().playOnAwake = false;
		}
		else
		{
			Script.gameObject.AddComponent<AudioSource>();
			Script.gameObject.GetComponent<AudioSource>().hideFlags = HideFlags.HideInInspector;
			Script.GetComponent<AudioSource>().playOnAwake = false;
		}
	}


    // Overrides the Inspector of the Audio Manager Script with this stuff...
    public override void OnInspectorGUI()
    {
        // The Path to the audio folder in your project
        string Dir = Application.dataPath + "/Audio";

        // Makes the audio directoy if it doesn't exist in your project
        // * This will not create a new folder if you already have an audio folder *
        // * As of V2 it will also create a new Audio Manager File if there isn't one in the audio folder *
        if (!Directory.Exists(Application.dataPath + "/Audio"))
        {
            AssetDatabase.CreateFolder("Assets", "Audio");

            if (!Directory.Exists(Application.dataPath + "/Audio"))
            {
                AssetDatabase.CreateFolder("Assets/Audio", "Files");
            }

            AudioManagerFile NewAMF = ScriptableObject.CreateInstance<AudioManagerFile>();
            AssetDatabase.CreateAsset(NewAMF, "Assets/Audio/Files/Audio Manager File.asset");
            Script.File = (AudioManagerFile)AssetDatabase.LoadAssetAtPath("Assets/Audio/Files/Audio Manager File.asset", typeof(AudioManagerFile));
        }
        else if (((Directory.Exists(Application.dataPath + "/Audio")) && (!Directory.Exists(Application.dataPath + "/Audio/Files"))))
        {
            AssetDatabase.CreateFolder("Assets/Audio", "Files");
            AudioManagerFile NewAMF = ScriptableObject.CreateInstance<AudioManagerFile>();
            AssetDatabase.CreateAsset(NewAMF, "Assets/Audio/Files/Audio Manager File.asset");
            Script.File = (AudioManagerFile)AssetDatabase.LoadAssetAtPath("Assets/Audio/Files/Audio Manager File.asset", typeof(AudioManagerFile));
        }
        else if (Directory.Exists(Application.dataPath + "/Audio/Files"))
        {
            string[] AllFiles = AssetDatabase.FindAssets("t:AudioManagerFile", new[] { "Assets/audio" });

            if (AllFiles.Length == 0)
            {
                AudioManagerFile NewAMF = ScriptableObject.CreateInstance<AudioManagerFile>();
                AssetDatabase.CreateAsset(NewAMF, "Assets/Audio/Files/Audio Manager File.asset");
                Script.File = (AudioManagerFile)AssetDatabase.LoadAssetAtPath("Assets/Audio/Files/Audio Manager File.asset", typeof(AudioManagerFile));
            }
        }

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        // Carter Games Logo
        if (Resources.Load<Texture2D>("CarterGames/Logo"))
        {
            if (GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Logo"), GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
            {
                GUI.FocusControl(null);
            }
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Carter Games", EditorStyles.boldLabel, GUILayout.MaxWidth(100));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Audio Manager | Version: 2.1");
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Documentation"))
        {
            Application.OpenURL("http://carter.games/audiomanager/");
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("File in use: ", GUILayout.MaxWidth(65));
        Script.File = (AudioManagerFile)EditorGUILayout.ObjectField(Script.File ,typeof(AudioManagerFile), false);
        EditorGUILayout.EndHorizontal();

        if (Script.File)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Prefab: ", GUILayout.MaxWidth(65));
            Script.File.Prefab = (GameObject)EditorGUILayout.ObjectField(Script.File.Prefab, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        GUILayout.Space(10);

        // Checks to see if the Audio Manager Library is not empty
        // * If its not empty then you've pressed scan before, therefore it won't show the scan button again *
        if (Script.File)
        {
            if (Script.File.Populated)
            {
                HasScannedOnce = true;
            }
        }
    

		// Changes the text & colour of the first button based on if you've pressed it before or not
		if (HasScannedOnce) { ScanButtonString = "Re-Scan"; GUI.color = ScannedCol; }
		else { ScanButtonString = "Scan"; GUI.color = ScanCol; }


		// Begins a grouping for the buttons to stay on one line
		EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

		// The actual Scan button - Runs functions to get the audio from the directory and add it to the library on the Audio Manager Script
		if (GUILayout.Button(ScanButtonString, GUILayout.Width(80)))
		{
            if (Script.File)
            {
                // Init Lists
                AudioList = new List<AudioClip>();
                AudioStrings = new List<string>();

                // Auto filling the lists 
                AddAudioClips();
                AddStrings();

                // Updates the lists 
                Script.File.ClipName = AudioStrings;
                Script.File.Clip = AudioList;

                Script.File.Populated = true;
            }
            else
            {
                Debug.LogAssertion("(*Audio Manager*): Please select a Audio Manager File before scanning, I can't scan without somewhere to put it all :)");
            }
		}

		// Resets the color of the GUI
		GUI.color = Color.white;


		// The actual Clear button - This just clear the Lists and Library in the Audio Manager Script and resets the Has Scanned bool for the Scan button reverts to green and "Scan"
		if (GUILayout.Button("Clear", GUILayout.Width(60)))
		{
            if (Script.File)
            {
                Script.Sound_Lib.Clear();
                Script.File.ClipName.Clear();
                Script.File.Clip.Clear();
                Script.File.Populated = false;
                HasScannedOnce = false;
            }
            else
            {
                Debug.Log("(*Audio Manager*): No Audio Manager File selected, ignoring request.");
            }
        }

		GUI.color = Color.white;

        // Ends the grouping for the buttons
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


        // *** Labels ***
        if (Script.File)
        {
            if (Script.File.Populated)
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Items Scanned:", EditorStyles.boldLabel, GUILayout.Width(105f));
                EditorGUILayout.LabelField(Script.File.Clip.Count.ToString());
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
            else
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("Some files are not scanned!");
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
        }

        if (HasScannedOnce)
		{
			DisplayNames();
		}

		Repaint();

        // Shows anything that would normally be on the inspector - unless it has the Hide From Inspector
        // * If uncommented this will show the normal inspector for the script as the custom editor, not recommended and shouldn't be needed *
        //base.OnInspectorGUI();
    }


	// Adds all the audio clips to the list
	private void AddAudioClips()
	{
		// Makes a new lsit the size of the amount of objects in the path
		List<string> AllFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/audio"));

		// Checks to see if there is anything in the path, if its empty it will not run the rest of the code and instead put a message in the console
		if (AllFiles.Count > 0)
		{
			HasScannedOnce = true;  // Sets the has scanned once to true so the scan button turns into the re-scan button

			AudioClip Source = null;

			foreach (string Thingy in AllFiles)
			{
				string Path = "Assets" + Thingy.Replace(Application.dataPath, "").Replace('\\', '/');

				if (AssetDatabase.LoadAssetAtPath(Path, typeof(AudioClip)))
				{
					Source = (AudioClip)AssetDatabase.LoadAssetAtPath(Path, typeof(AudioClip));
					AudioList.Add(Source);
				}
			}
		}
		else
		{
			// !Warning Message - shown in the console should there be no audio in the directory been scanned
			Debug.LogWarning("(*Audio Manager*): Please ensure there are Audio files in the directory: " + Application.dataPath + "/Audio");
		}
	}


	// Adds all the strings for the clip names to its list
	private void AddStrings()
	{
		for (int i = 0; i < AudioList.Count; i++)
		{
			if (AudioList[i] == null)
			{
				AudioList.Remove(AudioList[i]);
			}
		}

		int Ignored = 0;

		for (int i = 0; i < AudioList.Count; i++)
		{
			if (AudioList[i].ToString().Contains("(UnityEngine.AudioClip)"))
			{
				AudioStrings.Add(AudioList[i].ToString().Replace(" (UnityEngine.AudioClip)", ""));
			}
			else
			{
				Ignored++;
			}
		}

		if (Ignored > 0)
		{
			// This message should never show up, but its here just incase
			Debug.LogAssertion("(*Audio Manager*): " + Ignored + " entries ignored, this is due to the files either been in sub directories or other files that are not Unity AudioClips.");
		}
	}


	// Returns the number of files in the audio directory
	private int CheckNumberOfFiles()
	{
		int FinalCount = 0;
		List<AudioClip> ClipCount = new List<AudioClip>();
        List<string> AllFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/audio"));

        foreach (string Thingy in AllFiles)
        {
            if ((Thingy.Contains(".aif")) || (Thingy.Contains(".mp3")) || (Thingy.Contains(".wav")) || (Thingy.Contains(".ogg")))
            {
                FinalCount++;
            }
            else
            {
                // Ignore Thingy
            }
        }

		// divides the final result by 2 as it includes the .meta files in this count which we don't use
		return FinalCount / 2;
	}


	// Displayes all the audio clips with the name and a button to preview said clips
	private void DisplayNames()
	{
		// Used as a placeholder for the clip name each loop
		string Elements = "";

		// Going through all the audio clips and making an element in the Inspector for them
		for (int i = 0; i < Script.File.ClipName.Count; i++)
		{
			Elements = Script.File.ClipName[i];

			// Starts the ordering
			EditorGUILayout.BeginHorizontal(); 

			// Changes the GUI colour to green for the buttons
			GUI.color = ScanCol;

			// If there are no clips playing it will show "preview clip" buttons for all elements
			if (!Script.GetComponent<AudioSource>().isPlaying)
			{
                if (Resources.Load<Texture2D>("CarterGames/Play"))
                {
                    if (GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Play"), GUIStyle.none, GUILayout.Width(20), GUILayout.Height(20)))
                    {
                        Script.GetComponent<AudioSource>().clip = Script.File.Clip[i];
                        Script.GetComponent<AudioSource>().PlayOneShot(Script.GetComponent<AudioSource>().clip);
                    }
                }
                else
                {
                    if (GUILayout.Button("P", GUILayout.Width(20), GUILayout.Height(20)))
                    {
                        Script.GetComponent<AudioSource>().clip = Script.File.Clip[i];
                        Script.GetComponent<AudioSource>().PlayOneShot(Script.GetComponent<AudioSource>().clip);
                    }
                }
            }
			// if a clip is playing, the clip that is playing will have a "stop clip" button instead of "preview clip" 
			else if (Script.GetComponent<AudioSource>().clip == Script.File.Clip[i])
			{
				GUI.color = RedCol;

                if (Resources.Load<Texture2D>("CarterGames/Stop"))
                {
                    if (GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Stop"), GUIStyle.none, GUILayout.Width(20), GUILayout.Height(20)))
                    {
                        Script.GetComponent<AudioSource>().Stop();
                    }
                }
                else
                {
                    if (GUILayout.Button("S", GUILayout.Width(20), GUILayout.Height(20)))
                    {
                        Script.GetComponent<AudioSource>().Stop();
                    }
                }
			}
			// This just ensures the rest of the elements keep a button next to them
			else
			{
                if (Resources.Load<Texture2D>("CarterGames/Play"))
                {
                    if (GUILayout.Button(Resources.Load<Texture2D>("CarterGames/Play"), GUIStyle.none, GUILayout.Width(20), GUILayout.Height(20)))
                    {
                        Script.GetComponent<AudioSource>().clip = Script.File.Clip[i];
                        Script.GetComponent<AudioSource>().PlayOneShot(Script.GetComponent<AudioSource>().clip);
                    }
                }
                else
                {
                    if (GUILayout.Button("P", GUILayout.Width(20), GUILayout.Height(20)))
                    {
                        Script.GetComponent<AudioSource>().clip = Script.File.Clip[i];
                        Script.GetComponent<AudioSource>().PlayOneShot(Script.GetComponent<AudioSource>().clip);
                    }
                }
			}

			// Resets the GUI colour
			GUI.color = Color.white;

            // Adds the text for the clip
            EditorGUILayout.TextArea(Elements, GUILayout.ExpandWidth(true));

            // Ends the GUI ordering
            EditorGUILayout.EndHorizontal();
		}
	}

    private void OnInspectorUpdate()
    {
        Repaint();
    }
}
*/