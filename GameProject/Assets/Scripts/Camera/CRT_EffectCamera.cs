﻿using UnityEngine;
public class CRT_EffectCamera: A {
 public Material EffectMaterial;
 public bool m_CRTShaderOn = true;
 float time;
 public CRT_Preset preset;
 void OnRenderImage(RenderTexture src, RenderTexture dst) {
  EffectMaterial.SetFloat("scanlineCount", preset.scanlineCount);
  EffectMaterial.SetFloat("scanlineIntensity", preset.scanlineBrightness);
  EffectMaterial.SetFloat("scanlineSpeed", preset.scanlineSpeed);
  EffectMaterial.SetFloat("vignetteStrenght", preset.vignetteStrenght);
  EffectMaterial.SetFloat("noiseSize", preset.noiseSize);
  EffectMaterial.SetFloat("noiseAmount", preset.noiseAmount);
  EffectMaterial.SetFloat("interlaceStrenght", preset.flickerEffectStrenght);
  EffectMaterial.SetFloat("time", Time.fixedTime);
  if (m_CRTShaderOn) Graphics.Blit(src, dst, EffectMaterial);
  else Graphics.Blit(src, dst);
 }
}