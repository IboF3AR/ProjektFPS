using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Custom/Items/Key")]
public class Item_Key : Item
{

    public bool shouldBeRemovedAfterUse = true;

    public override void Use()
    {
        if(shouldBeRemovedAfterUse)
        {
            Inventory.Inst.Remove_Item(this, 1);
        }
    } 
}
