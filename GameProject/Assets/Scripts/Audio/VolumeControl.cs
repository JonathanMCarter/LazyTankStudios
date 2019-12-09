using UnityEngine;
using UnityEngine.UI;
public class VolumeControl: A {
Slider slider;
void Start() {
slider = G<Slider> ();
UpdateVolume();}
public void UpdateVolume() {
AudioListener.volume = slider.value;}
public void ToggleMute() {
AudioListener.pause = !AudioListener.pause;}}