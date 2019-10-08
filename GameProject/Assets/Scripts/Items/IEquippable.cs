using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//subject to change
public enum EQUIP_SLOT { LEFT_HAND, RIGHT_HAND, BOTH_HANDS, HEAD, CHEST, LEGS, FEET, ACESSORY };

public interface IEquippable
{
    EQUIP_SLOT getSlot();
    //Use these to do something when equipping or unequipping the item like applying/removing stats or an effect (Will need player parameter)
    void onEquip();
    void onUnequip();
}
