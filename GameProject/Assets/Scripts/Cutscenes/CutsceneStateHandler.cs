﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Created by: Toby Wishart
 * Last edit: 29/10/19
 */
public class CutsceneStateHandler : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(placeholderCutsceneTimer(5));
    }

    void Update()
    {
        
    }


    //For testing purposes so the cutscene can end
    private IEnumerator placeholderCutsceneTimer(float time)
    {
        yield return new WaitForSeconds(time);
        FinishCS();
    }

    //Call when the cutscene has finished, make sure this always gets called otherwise the player will be stuck in the cutscene
    public void FinishCS()
    {
        //Do things before unloading the scene

        //Unload the scene so dialogue can continue
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }

}