using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Custom/Items/Default")]
public class Item : ScriptableObject
{
    public ItemTypes itemType;
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public string description = "Item description";

    public virtual void Use()
    {

    }
}
