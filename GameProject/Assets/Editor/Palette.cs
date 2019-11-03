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

[CreateAssetMenu(fileName = "Palettes", menuName = "Palettes")]
public class Palette : ScriptableObject
{
    public List<Color32> Pal1 = new List<Color32>(2) { new Color32(97, 195, 105, 255), new Color32(45, 161, 33, 255), new Color32(23, 115, 18, 255) };
    public List<Color32> Pal2 = new List<Color32>(2) { new Color32(255, 255, 255, 255), new Color32(133, 190, 192, 255), new Color32(56, 133, 137, 255) };
    public List<Color32> Pal3 = new List<Color32>(2) { new Color32(240, 250, 209, 255), new Color32(214, 224, 117, 255), new Color32(152, 148, 61, 255) };
    public List<Color32> Pal4 = new List<Color32>(2) { new Color32(132, 185, 107, 255), new Color32(71,132, 41, 255), new Color32(28, 84, 0, 255) };
}