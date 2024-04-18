using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    protected Animator animator;
    AI_Movement aI_Movement;
    bool isInAction  = false;

    public AI_Action[] allActions;

    void Start()
    {
        aI_Movement = GetComponent<AI_Movement>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        aI_Movement.Tick();
        HandleActions();
        aI_Movement.Set_IsBlockedByAction(isInAction);
    }

    void HandleActions()
    {
        isInAction = animator.GetBool("IsPlaying");
        foreach (var item in allActions)
        {
            item.animationIsPlaying = isInAction;
            item.Tick();
        }
    }

}
