using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Custom/Items/Healing")]
public class Item_Consumeable_Healing : Item
{
    [Header("Healer")]
    public int healValue;

    public override void Use()
    {
        GameManager.Inst.player.GetComponent<Player_Values>().Heal(healValue);
    }
}
