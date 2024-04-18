using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class IAC_ItemPickUp : MonoBehaviour
{
    public Item itemToGive;
    public int amountOfItemToGive = 1;
    private bool hasBeenLooted  = false;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().e_Interact += PerformAction; 
    }

    public void PerformAction()
    {
        if(!hasBeenLooted)
        {
           Inventory.Inst.Add_Item(itemToGive, amountOfItemToGive);
        }
    }
}
