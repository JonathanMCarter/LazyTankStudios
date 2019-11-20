using UnityEngine;
using UnityEngine.UI;
public class CRT_DropdownSwitch: A {
 public CRT_Preset[] presets;
 private CRT_EffectCamera effectCamera;
 private Dropdown dropdown;
 void Start() {
  effectCamera = FindObjectOfType < CRT_EffectCamera > ();
  dropdown = GetComponent < Dropdown > ();
 }
 public void SwitchPreset() {
  if (dropdown.value < presets.Length && presets[dropdown.value] != null) effectCamera.preset = presets[dropdown.value];
 }
}