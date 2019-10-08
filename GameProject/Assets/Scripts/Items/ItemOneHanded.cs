using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New One Handed Weapon", menuName = "Items/Weapons/One Handed", order = 1)]
public class ItemOneHanded : ItemWeapon {
    public override EQUIP_SLOT getSlot()
    {
        return EQUIP_SLOT.RIGHT_HAND;
    }

    public override void onAttack()
    {

    }

    public override void onEquip()
    {

    }

    public override void onUnequip()
    {

    }
}
