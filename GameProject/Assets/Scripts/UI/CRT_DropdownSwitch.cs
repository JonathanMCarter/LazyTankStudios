﻿
using UnityEngine;
using UnityEngine.UI;

/*
 * CRT Dropdown Switch 
 * 
 *  Changes the preset of the Camera Depending on the option of the Dropdown this script is attached to
 *  ARRAY NEEDS TO BE IN THE SAME ORDER AS THE DROPDOWN OPTIONS !!!!
 *
 * Owner: Andreas Kraemer
 * Last Edit : 10/10/19
 * 
 * Also Edited by : <Enter name here if you edit this script>
 * Last Edit: <Date here if you edit this script>
 * 
 * */

public class CRT_DropdownSwitch : A
{
    [Tooltip("Array of CRT_Presets needs to be in the same order as the Dropdown options")]
    public CRT_Preset[] presets;
    private CRT_EffectCamera effectCamera;
    private Dropdown dropdown;

    void Start()
    {
        effectCamera=FindObjectOfType<CRT_EffectCamera>();
        dropdown=GetComponent<Dropdown>();
    }

    ///<summary>
    ///Used to switch the CRT_Preset variable of the camera for the one selected in the dropdown
    ///</summary>
    public void SwitchPreset()
    {
        if(dropdown.value<presets.Length&&presets[dropdown.value]!=null)effectCamera.preset=presets[dropdown.value];
    }
}
