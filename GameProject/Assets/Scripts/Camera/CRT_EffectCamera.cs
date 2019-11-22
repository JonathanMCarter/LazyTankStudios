using UnityEngine;
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
    //Snap camera pos
    void Update()
    {
        float scale = 8 * Mathf.Max(1, Mathf.Min((Screen.height / 108), (Screen.width / 192)));
        G<Camera>().orthographicSize = (Screen.height / 2) / scale;
        float units = 1.0F / scale;
        Vector3 pp = F("Hero").transform.position;
        transform.position = new Vector3(Mathf.Round(pp.x / units) * units, Mathf.Round(pp.y / units) * units, transform.position.z);
    }
}