using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/*
    You shouldn't be here.....
    If something throws an error that stops you working then let me know...


    Item Creator Editor Script
    -=-=-=-=-=-=-=-=-=-=-=-

    Made by: Jonathan Carter
    Last Edited By: Jonathan Carter
    Date Edited Last: 13/10/19 - Script Made


    This script allows the designners to spawn prefabs using shortcuts or from a menu item.

*/

public class ItemCreator : Editor
{
    // Spawns Inventory Items with all needed components as well as the Colour Change shader attached (you will need to instance it still though).
    [MenuItem("Tools/Spawn New Item %&i", priority = 1)]
    public static void SpawnObject()
    {
        GameObject NewOBJ;

        List<string> AllFiles = new List<string>(Directory.GetFiles(Application.dataPath + "/prefabs/items/"));
        string Path;

        foreach (string Thingy in AllFiles)
        {
            Path = "Assets" + Thingy.Replace(Application.dataPath, "").Replace('\\', '/');
            NewOBJ = (GameObject)AssetDatabase.LoadAssetAtPath(Path, typeof(GameObject));
            PrefabUtility.InstantiatePrefab(NewOBJ);
        }
    }
}
