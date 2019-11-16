﻿
using UnityEngine;

/*
 * CRT Preset ScriptableObject
 * 
 *  Used to create presets for the CRT shader
 *
 * Owner: Andreas Kraemer
 * Last Edit : 10/10/19
 * 
 * Also Edited by : <Enter name here if you edit this script>
 * Last Edit: <Date here if you edit this script>
 * 
 * */

[CreateAssetMenu]
public class CRT_Preset : ScriptableObject
{
    public float scanlineCount,scanlineBrightness,vignetteStrenght,scanlineSpeed,noiseSize,noiseAmount,flickerEffectStrenght;
}
