using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Interactable))]
public class IAC_PlayAnimation : MonoBehaviour
{
    Animator animator;
    bool shouldReplay  = false;
    bool hasPlayed = false; // info: ignore if shouldReplay is true;
    public string animation_ParameterString;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<Interactable>().e_Interact += Interact;
    }

    public void Interact()
    {
        if(!shouldReplay && !hasPlayed)
        {
            animator.SetTrigger(animation_ParameterString);
            hasPlayed  = true;
        } else if(shouldReplay)
        {
            animator.SetTrigger(animation_ParameterString);
        }
    }
}
