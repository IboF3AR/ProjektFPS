using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Custom/Items/Ammuntion")]
public class Item_Ammunition : Item
{
    [Header("Ammuntion")]
    public Weapon weapon;

    //public int damageModifier
    //public DamageType damageType = DamageType.Fire;
}
