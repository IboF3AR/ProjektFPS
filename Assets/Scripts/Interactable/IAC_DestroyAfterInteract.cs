using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class IAC_DestroyAfterInteract : MonoBehaviour
{
    public float waitTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().e_Interact += PerformAction; 
    }

    public void PerformAction()
    {
        Destroy(this.gameObject, waitTime);
    }
}
