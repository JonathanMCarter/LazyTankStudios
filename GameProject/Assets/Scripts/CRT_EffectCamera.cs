using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRT_EffectCamera : MonoBehaviour
{
    public Material EffectMaterial;
    public bool IsCRTShaderOn=true;
    public float ScanlineCount=1000.0f;
    public float ScanlineIntesity=0.05f;
    public float VignetteStrenght=0.3f;
    public float ScanlineSpeed=10.0f;

void Start()
{
    //if the values are required to change during runtime paste the lines below at the beginning of OnRenderImage()
    EffectMaterial.SetFloat("scanlineCount",ScanlineCount);
    EffectMaterial.SetFloat("scanlineIntensity",ScanlineIntesity);
    EffectMaterial.SetFloat("scanlineSpeed",ScanlineSpeed);
    EffectMaterial.SetFloat("vignetteStrenght",VignetteStrenght);
}
void OnRenderImage(RenderTexture src,RenderTexture dst)
    {
        EffectMaterial.SetFloat("time",Time.fixedTime);
        if(IsCRTShaderOn)Graphics.Blit(src,dst,EffectMaterial);
        else Graphics.Blit(src,dst);
    }
}
