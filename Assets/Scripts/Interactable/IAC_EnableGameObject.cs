using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class IAC_EnableGameObject : MonoBehaviour
{
    public GameObject objectToEnable;

    void Start()
    {
        GetComponent<Interactable>().e_Interact += Interact;
    }

    public void Interact()
    {
        objectToEnable.SetActive(true);
    }
}
