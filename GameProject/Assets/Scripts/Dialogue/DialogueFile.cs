using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Dialouge File Scriptable Object
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 6/10/19 - To add this comment bit in (nothing else was changed)

    While this could be defined in the same script, I kept it seperate for now,
    This just has 2 list of string that hold the dialogue you input from the editor window

*/

/// <summary>
/// Dialouge File Scriptable Object.
/// 
/// This scriptable object is used to create dialogue list collections which are read by the main dialogue script to be used in the game.
/// @note If this is edited at all the whole dialogue system may break.
/// </summary>

[CreateAssetMenu(fileName = "Dialogue File", menuName = "Carter Games/Dialogue File")]
public class DialogueFile : ScriptableObject
{
    /// <summary>
    /// Dialogue Names.
    /// This holds the names of the characters talking in the dialogue.
    /// </summary>
    public List<string> Names;
    /// <summary>
    /// Dialogue Text.
    /// This holds the actual dialogue of the characters talking in the game.
    /// </summary>
    public List<string> Dialogue;
}
