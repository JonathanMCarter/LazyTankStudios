using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ItemWeapon : Item, IEquippable
{
    public int damageMin;
    public int damageMax;

    public abstract void onEquip();
    public abstract void onUnequip();
    public abstract EQUIP_SLOT getSlot();

    //Apply additional effects like chance to stun, poison, etc. (Might need a target parameter in the future)
    public abstract void onAttack();
}
