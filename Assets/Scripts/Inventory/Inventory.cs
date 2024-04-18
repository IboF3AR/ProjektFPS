using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Inst;

    void Awake()
    {
        if(Inst != null) return;
        Inst = this;
    }
    #endregion

    public Inventory_UI inventory_ui;
    public Dictionary<Item, int> items = new Dictionary<Item, int>();
    public event Action<Item, bool, int> e_OnItemChanged;
    public Item pickeditem;


    public void Add_Item (Item item, int amount)
    {
        if(!item.isDefaultItem)
        {
            if(items.ContainsKey(item))
            {
                items[item] += amount; 
            }
            else
            {
                items.Add(item,amount);
            }
            e_OnItemChanged?.Invoke(item, true, amount);
            if(item is Item_Ammunition)
            {
                GameManager.Inst.player.GetComponent<Player_HandleWeapon>().OnAmmunitionPickedUp(item);
            }
        } 
    }

    public void Remove_Item (Item item, int amount)
    {
        if(items.ContainsKey(item))
        {
            items[item] -= amount;
            if(items[item] <= 0)
            {
                items.Remove(item);
            } 
        }
        e_OnItemChanged?.Invoke(item, false, amount);
        
    } 

    public void SetPickedItem(Item item)
    {
        pickeditem = item;
    }

    public void UsePickedItem()
    {
        if(pickeditem != null) 
        {
            pickeditem.Use();
            if(pickeditem.itemType == ItemTypes.Consumeable)
            {
                Remove_Item(pickeditem, 1);
                pickeditem = null;
            }
        }
    }
    
}
