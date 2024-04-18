using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_HitReaction : AI_Action
{
    private Character_Values character_Values;
    private AI_Movement aI_Movement;
    public string animation_parameterString = "Hit_ReactionHead";

    public Damageable damageable;

    public float staggerValue = 540;
    [SerializeField] private float currentStagger;
    public float staggerDecreaseFactor  = 1.5f;

    private void Start()
    {
        aI_Movement = GetComponent<AI_Movement>();
        animator = GetComponentInChildren<Animator>();
        character_Values = GetComponentInChildren<Character_Values>();
        damageable.e_takeDamage += OnTakeHit;
    }

    public override void Tick()
    {
        if(currentStagger > 0)
        currentStagger -= Time.deltaTime * staggerDecreaseFactor;
    }

    public void OnTakeHit(int damage)
    {
        // Reaction base on hit
        animator.ResetTrigger(animation_parameterString);
        if(character_Values.isAlive)
        {
            currentStagger += (float) damage;
            if(currentStagger > staggerValue)
            {
                PerformAction();
                currentStagger = 0f;
            }
        } 
    }

    public override void PerformAction()
    {
         animationIsPlaying = true;
         animator.SetTrigger(animation_parameterString);
    }
}
