using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptableWeapon : ScriptableObject
{
    [SerializeField]
    public string itemName;
    public string ItemName { get; }
    [SerializeField]
    public string description;
    public string Description { get; }
    [SerializeField]
    public Image icon;
    public Image Icon { get; }
}
