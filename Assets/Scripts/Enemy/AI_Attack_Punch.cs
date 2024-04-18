using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attack_Punch : AI_Action
{
    private AI_Movement aI_Movement;
    public string animation_parameterString = "IsInRange";
    public Collider theCollider;
    public DamageCollider damageCollider;
    [Header("Values")]
    public int damage_values;
    public bool isInRange  = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        aI_Movement = GetComponent<AI_Movement>();
    }

    public override void Tick()
    {
        if(aI_Movement.isInRange && !animationIsPlaying)
        {
            PerformAction();
        } else 
        {
            animator.SetBool(animation_parameterString, false);
        }
        CheckForAnimationPlaying();   
    }

    private void CheckForAnimationPlaying()
    {
        if(!animationIsPlaying)
        {
            DisableDamageCollider();
        }
    }

    public override void PerformAction()
    {
        animationIsPlaying = true;
        animator.SetBool(animation_parameterString, true);
        damageCollider.damage = damage_values;
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }
}
