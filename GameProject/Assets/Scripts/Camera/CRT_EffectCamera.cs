using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CRT Effect Script 
 * 
 *  Controls the effects of the CRT shader 
 *
 * Owner: Andreas Kraemer
 * Last Edit : 1/10/19
 * 
 * Also Edited by : <Enter name here if you edit this script>
 * Last Edit: <Date here if you edit this script>
 * 
 * */

public class CRT_EffectCamera : MonoBehaviour
{
    public Material EffectMaterial;
    public bool m_CRTShaderOn=true;
    public float scanlineCount=500.0f;
    public float scanlineBrightness=1f;
    public float vignetteStrenght=0.3f;
    private float time;
    public float scanlineSpeed=10.0f;
    public float noiseSize=5;
    public float noiseAmount=1f;
    public float flickerEffectStrenght=5;

void OnRenderImage(RenderTexture src,RenderTexture dst)
    {
        EffectMaterial.SetFloat("scanlineCount",scanlineCount);
        EffectMaterial.SetFloat("scanlineIntensity",scanlineBrightness);
        EffectMaterial.SetFloat("scanlineSpeed",scanlineSpeed);
        EffectMaterial.SetFloat("vignetteStrenght",vignetteStrenght);
        EffectMaterial.SetFloat("noiseSize",noiseSize);
        EffectMaterial.SetFloat("noiseAmount",noiseAmount);
        EffectMaterial.SetFloat("interlaceStrenght",flickerEffectStrenght);
        EffectMaterial.SetFloat("time",Time.fixedTime);
        if(m_CRTShaderOn)Graphics.Blit(src,dst,EffectMaterial);
        else Graphics.Blit(src,dst);
    }
}
