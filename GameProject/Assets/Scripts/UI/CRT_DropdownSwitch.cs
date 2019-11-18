using UnityEngine;
using UnityEngine.UI;
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
    public void SwitchPreset()
    {
        if(dropdown.value<presets.Length&&presets[dropdown.value]!=null)effectCamera.preset=presets[dropdown.value];
    }
}
