﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTransitionEffectCamera : MonoBehaviour
{
    public Material TransitionMaterial;
    float transition=0;
    bool increase=true;
    public float transitionSpeed=1f;
    public float transitionDuration=1f;
    public bool effectIsPlaying;
    // Start is called before the first frame update

    void FixedUpdate()
    {
        //this is to continue playing the effect once PlayEffect has been called
        if(effectIsPlaying)PlayEffect();
    }

    // Update is called once per frame
    void OnRenderImage(RenderTexture src,RenderTexture dst)
    {
        if(TransitionMaterial!=null)Graphics.Blit(src,dst,TransitionMaterial);   
    }
    //Call PlayEffect to start playing the entire effect with fade-out and fade-in
    void PlayEffect()
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
