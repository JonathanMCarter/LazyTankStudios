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

[CreateAssetMenu(fileName = "Dialogue File", menuName = "Carter Games/Dialogue File")]
public class DialogueFile : ScriptableObject
{
    public List<string> Names;
    public List<string> Dialogue;
}
