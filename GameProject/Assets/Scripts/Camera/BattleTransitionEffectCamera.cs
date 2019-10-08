using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Battle Transition Effect Script 
 * 
 *  Controls the speed and duration of the battle transition, call PlayEffect() to start the Transition
 *
 * Owner: Andreas Kraemer
 * Last Edit : 5/10/19
 * 
 * Also Edited by : Lewis Cleminson
 * Last Edit: 07.10.19
 * Reason: Change FixedUpdate to Update. Investigating black screen problems.
 * 
 * */

public class BattleTransitionEffectCamera : MonoBehaviour
{
    public Material TransitionMaterial;
    float transition=0;
    bool increase=true;
    public float transitionSpeed=1f;
    public float transitionDuration=1f;
    public bool effectIsPlaying;

    private void Awake() //called before any Start Method. Will clear the Cutoff float in material to try and fix black screen issue. Added by LC
    {
        TransitionMaterial.SetFloat("_Cutoff", 0);
    }


    private void Update()
    {
        if (effectIsPlaying) PlayEffect();
    }
    //void FixedUpdate() //This should be Update() not FixedUpdate(). Fixedupdate is when Unity updates the physics system and is not run at regular intervals despite the name. Update runs once per frame whenever stuff is drawn on screen.
    //{
    //    //this is to continue playing the effect once PlayEffect has been called
    //    if(effectIsPlaying)PlayEffect();
    //}

    // Update is called once per frame
    void OnRenderImage(RenderTexture src,RenderTexture dst)
    {
        if(TransitionMaterial!=null)Graphics.Blit(src,dst,TransitionMaterial);   
    }

    ///Call PlayEffect to start playing the entire effect with fade-out and fade-in
    public void PlayEffect()
    {
        effectIsPlaying=true;
        if(transition>=transitionDuration)increase=false;

        if(increase)transition+=transitionSpeed/100;
        else transition-=transitionSpeed/100;

        TransitionMaterial.SetFloat("_Cutoff",transition);

        if(transition<=0)
        {
            increase=true;
            effectIsPlaying=false;
        }
    }
}
