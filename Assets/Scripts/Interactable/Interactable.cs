using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Interactable : MonoBehaviour
{
    public event Action e_Interact;
    public bool deactivateAfterUsed  = true;
    public string description;
    
    public void Interact(){
        if(deactivateAfterUsed)
        {
            gameObject.layer = default;
            e_Interact?.Invoke();
            this.enabled  = false;
        }
        else
        {
           e_Interact?.Invoke();
        }
    }

    public string GetDescription()
    {
        return description;
    }
   
}
