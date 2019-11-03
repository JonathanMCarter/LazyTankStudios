using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Palette SO - olds the universial palettes for the game
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 03/11/19 - This was made :)

*/

/// Palette Scriptable object used for the palettes in the game
[CreateAssetMenu(fileName = "Palettes", menuName = "Palettes")]
public class Palette : ScriptableObject
{
    /// (Plains Palette?) The first of four palettes the game is allowed to have, these are predefined so when you create a new palette scriptable object the colours are set for you to some respect. 
    public List<Color32> Pal1 = new List<Color32>(2) { new Color32(97, 195, 105, 255), new Color32(45, 161, 33, 255), new Color32(23, 115, 18, 255) };
    /// (Snow Palette?) The second of four palettes the game is allowed to have, these are predefined so when you create a new palette scriptable object the colours are set for you to some respect.
    public List<Color32> Pal2 = new List<Color32>(2) { new Color32(255, 255, 255, 255), new Color32(133, 190, 192, 255), new Color32(56, 133, 137, 255) };
    /// (Desert Palette?) The third of four palettes the game is allowed to have, these are predefined so when you create a new palette scriptable object the colours are set for you to some respect.
    public List<Color32> Pal3 = new List<Color32>(2) { new Color32(240, 250, 209, 255), new Color32(214, 224, 117, 255), new Color32(152, 148, 61, 255) };
    /// (Forest Palette?) The fourth of four palettes the game is allowed to have, these are predefined so when you create a new palette scriptable object the colours are set for you to some respect.
    public List<Color32> Pal4 = new List<Color32>(2) { new Color32(132, 185, 107, 255), new Color32(71,132, 41, 255), new Color32(28, 84, 0, 255) };
}