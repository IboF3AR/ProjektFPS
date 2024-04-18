using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class IAC_Test : MonoBehaviour
{
    public string textToDebug = "TEST";
    void Start()
    {
        GetComponent<Interactable>().e_Interact += Interact;
    }

    public void Interact()
    {
        Debug.Log(textToDebug);
    }
}
