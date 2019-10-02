using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRT_EffectCamera : MonoBehaviour
{
    public Material EffectMaterial;
    public bool m_CRTShaderOn=true;
    public float scanlineCount=1000.0f;
    public float scanlineIntesity=1f;
    public float vignetteStrenght=0.3f;
    private float time;
    public float scanlineSpeed=10.0f;
    public float noiseSize=5;
    public float noiseAmount=1f;
    public float interlaceStrength=5;

void Start()
{
    
}
void OnRenderImage(RenderTexture src,RenderTexture dst)
    {
        EffectMaterial.SetFloat("scanlineCount",scanlineCount);
    EffectMaterial.SetFloat("scanlineIntensity",scanlineIntesity);
    EffectMaterial.SetFloat("scanlineSpeed",scanlineSpeed);
    EffectMaterial.SetFloat("vignetteStrenght",vignetteStrenght);
    EffectMaterial.SetFloat("noiseSize",noiseSize);
    EffectMaterial.SetFloat("noiseAmount",noiseAmount);
    EffectMaterial.SetFloat("interlaceStrenght",interlaceStrength);
        EffectMaterial.SetFloat("time",Time.fixedTime);
        if(m_CRTShaderOn)Graphics.Blit(src,dst,EffectMaterial);
        else Graphics.Blit(src,dst);
    }
}
