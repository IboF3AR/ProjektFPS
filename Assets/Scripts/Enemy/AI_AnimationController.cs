using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AnimationController : MonoBehaviour
{
    private Animator animator;
    private AI_Movement aI_Movement;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        aI_Movement = GetComponentInChildren<AI_Movement>();
    }

    private void Update()
    {
        animator.SetBool("isMoving",aI_Movement.isMoving);
    }

    public void TriggerAnimation(string animation_name)
    {
        animator.SetTrigger(animation_name);
    }

}
