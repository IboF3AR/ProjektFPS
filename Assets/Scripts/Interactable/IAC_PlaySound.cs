using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Interactable))]
public class IAC_PlaySound : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        GetComponent<Interactable>().e_Interact += Interact;   
    }

    public void Interact()
    {
        audioSource.Play();
    }
}
