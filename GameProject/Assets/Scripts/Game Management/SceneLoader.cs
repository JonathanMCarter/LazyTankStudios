using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Scene Loader Script
 * 
 *  can be used to load any scene
 *
 * Owner: Andreas Kraemer
 * Last Edit : 19/10/19
 * 
 * */


public class SceneLoader : MonoBehaviour
{
    ///<summary>
    ///Loads a Scene with the Name in the SceneName variable
    ///</summary>
    public void LoadSceneByName(string SceneName)
    {
        if(SceneName!=null&&SceneName!="")SceneManager.LoadScene(SceneName);
    }
}
