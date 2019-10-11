using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Inventory", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public void Use()
    {
        Debug.Log($"{Name} used.");
    }
}
