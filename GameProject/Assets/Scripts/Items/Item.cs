using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//might not be needed
public enum ITEM_TYPE { CONSUMABLE, WEAPON, ARMOUR, KEYITEM };

public class Item : ScriptableObject
{
    [SerializeField]
    public string itemName;
    public string itemDescription;
    public Sprite icon;
    public ITEM_TYPE type;
    public int stackSize = 99;

}
