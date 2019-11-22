using UnityEngine;
using UnityEngine.UI;
public class CRT_DropdownSwitch: A {
 public CRT_Preset[] presets;
 private CRT_EffectCamera effectCamera;
 private Dropdown dropdown;
 void Start() {
  effectCamera = F<CRT_EffectCamera>();
  dropdown = G<Dropdown>();
 }
 public void SwitchPreset() {
  if (dropdown.value < presets.Length && presets[dropdown.value] != null) effectCamera.preset = presets[dropdown.value];
 }
}