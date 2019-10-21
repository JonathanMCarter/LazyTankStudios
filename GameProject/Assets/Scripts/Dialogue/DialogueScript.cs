using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Dialouge Script
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 6/10/19 - To add this comment bit in (nothing else was changed)

    This script handles the dialouge manager, its not finished as I put it into the project as this was a summer project I didn't finish.
    So, some things may not work...

*/

public enum Styles
{
    Default,
    TypeWriter,
    Custom,
};

public class DialogueScript : MonoBehaviour
{
    // The Active Text File - This is used to populate the list when updated
    [Header("Current Dialouge File")]
    [Tooltip("This is the current dialouge text file selected by the script, if this isn't the file you called then something has gone wrong.")]
    //public TextAsset InputText;
    public DialogueFile File;

	// References to the displayed name and text area
	[Header("UI Element For Story Character Name")]
	[Tooltip("The UI Text element that is going to be used in your project to hold the Story Characters Name when they are talking.")]
	public Text DialName;

	[Header("UI Element that holds the character dialogue")]
	[Tooltip("The UI Text element that is going to hold the lines of dialouge for you story charcters.")]
	public Text DialText;

	// Int to check which element in the Dialogue list is next to be displayed
	private int DialStage = 0;

	// Checks is a courutine is running or not
	private bool IsCoRunning;

    public Styles DisplayStyle;

    public bool InputPressed;
    public bool RequireInput = true;
    public bool FileHasEnded = false;

	[Header("Characters used to define file read settings")]
	[Tooltip("This should match what you inputted into the 'File Read Settings' char after name, which controls where a story character's name ends in the dialouge files the script reads.")]
	public string NameChar = ":";
	[Tooltip("This should match what you inputted into the 'File Read Settings' char for new line, which controls where a story character's line of dialouge ends and a new one begins.")]
	public string NewLineChar = "#";

	[Header("Type Writer Settings")]
	public int TypeWriterCount = 1;


    // Stuff for events 'n' stuff 
    private Coroutine PauseCo;
    public Animation AnimToPlay;

    private void Update()
    {
        if (RequireInput)
        {
            switch (DisplayStyle)
            {
                case Styles.Default:

                    if (InputPressed)
                    {
                        DisplayNextLine();
                    }

                    break;
                case Styles.TypeWriter:

                    if ((!IsCoRunning) && (InputPressed))
                    {
                        StartCoroutine(TypeWriter(.005f));
                    }

                    break;
                case Styles.Custom:

                    if (InputPressed)
                    {
                        // your function here
                    }

                    break;
                default:
                    break;
            }
        }
    }


    // Changes the active file in the script
    public void ChangeFile(DialogueFile Input)
    {
        File = Input;
        Reset();
    }


    // Reads the next line of the dialogue sequence
    public void DisplayNextLine()
    {

        if (DialStage < File.Names.Count)
        {
            switch (File.Names[DialStage])
            {
                // Cinematic
                case "###":
                    // Put code here to run cinematic
                    break;

                // Pause
                case "@@@":
                    // Pauses dialogue for a little bit (for dramatic effect..............................................)
                    if (PauseCo == null)
                    {
                        PauseCo = StartCoroutine(PauseDial(3));
                    }
                    break;

                // Play Animation
                case "^^^":
                    // Put code to play animation
                    break;

                // End Dialogue
                case "***":
                    DialStage = 0;
                    FileHasEnded = true;
                    break;

                // Read Dial as normal
                default:
                    DialName.text = File.Names[DialStage];
                    DialText.text = File.Dialogue[DialStage];
                    DialStage++;
                    InputPressed = false;
                    break;
            }
        }
        else
        {
            DialName.text = "";
            DialText.text = "";
            FileHasEnded = true;
        }
    }

    // Display Option - Type Writer Style
    private IEnumerator TypeWriter(float Delay)
    {
        IsCoRunning = true;

        string Sentence = "";

        if (DialStage < File.Names.Count)
        {
            if (File.Dialogue[DialStage] != null)
            {
                Sentence = File.Dialogue[DialStage].ToString().Substring(0, TypeWriterCount);
            }

            if (Sentence.Length == File.Dialogue[DialStage].Length)
            {
                Sentence = File.Dialogue[DialStage].ToString();
                DialText.text = Sentence;
                InputPressed = false;
                DialStage++;
                TypeWriterCount = 0;
            }
            else
            {
                DialName.text = File.Names[DialStage];
                DialText.text = Sentence;
                TypeWriterCount++;
            }
        }
        else
        {
            DialName.text = "";
            DialText.text = "";
            FileHasEnded = true;
        }

        yield return new WaitForSeconds(Delay);

        IsCoRunning = false;
    }



    public void Input()
    {
        if (!InputPressed) { InputPressed = true; }
    }



    public void Reset()
    {
        if (InputPressed) { InputPressed = false; }
        if (FileHasEnded) { FileHasEnded = false; }
        DialStage = 0;
    }


    private IEnumerator PauseDial(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        ++DialStage;
    }
}
