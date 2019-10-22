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
    private float time;

    public CRT_Preset preset;


void OnRenderImage(RenderTexture src,RenderTexture dst)
    {
        EffectMaterial.SetFloat("scanlineCount",preset.scanlineCount);
        EffectMaterial.SetFloat("scanlineIntensity",preset.scanlineBrightness);
        EffectMaterial.SetFloat("scanlineSpeed",preset.scanlineSpeed);
        EffectMaterial.SetFloat("vignetteStrenght",preset.vignetteStrenght);
        EffectMaterial.SetFloat("noiseSize",preset.noiseSize);
        EffectMaterial.SetFloat("noiseAmount",preset.noiseAmount);
        EffectMaterial.SetFloat("interlaceStrenght",preset.flickerEffectStrenght);
        EffectMaterial.SetFloat("time",Time.fixedTime);
        if(m_CRTShaderOn)Graphics.Blit(src,dst,EffectMaterial);
        else Graphics.Blit(src,dst);
    }
}
