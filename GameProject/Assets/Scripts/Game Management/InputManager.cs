﻿using System.Collections;using UnityEngine;public class InputManager : A{
float xAxis, yAxis; bool fire1Clicked, fire2Clicked, fire3Clicked, delay;
#if !UNITY_ANDROID
void Start(){HideMobileUI(); }
#endif
public void SetX(int i) { xAxis = i; }
public void SetY(int i) { yAxis = i; }
void HideMobileUI() { GameObject[] phoneUI = GameObject.FindGameObjectsWithTag("MobileUI"); foreach (GameObject GO in phoneUI) GO.gameObject.SetActive(false); }
public void Fire1Clicked() { if (delay) return; fire1Clicked = true; SC(ClearButtons()); }
public void Fire2Clicked() { if (delay) return; fire2Clicked = true; SC(ClearButtons()); }
public void Fire3Clicked() { if (delay) return; fire3Clicked = true; SC(ClearButtons()); }
IEnumerator ClearButtons() {
yield return new WaitForEndOfFrame();
fire1Clicked = false;
fire2Clicked = false;
fire3Clicked = false;
}
public float X_Axis()
{
#if UNITY_ANDROID
return xAxis;
#endif
#if UNITY_WEBGL || UNITY_STANDALONE_WIN
float x = Input.GetAxisRaw("Horizontal");
if (x < 0.6f && x > -0.6f) return 0;
return Input.GetAxisRaw("Horizontal");
#endif
}
public float Y_Axis()
{
#if UNITY_ANDROID
return yAxis;
#endif
#if UNITY_WEBGL || UNITY_STANDALONE_WIN
float x = Input.GetAxisRaw("Vertical");
if (x < 0.6f && x > -0.6f) return 0;return Input.GetAxisRaw("Vertical");
#endif
}
public bool Button_A()
{
#if UNITY_ANDROID
return fire1Clicked;
#else
return Input.GetButtonDown("Fire1");
#endif
}
public bool Button_B()
{
#if UNITY_ANDROID
return fire2Clicked;
#else
return Input.GetButtonDown("Fire2");
#endif
}
public bool Button_Menu()
{
#if UNITY_ANDROID
return fire3Clicked;
#else
return Input.GetButtonDown("Fire3");
#endif
}}