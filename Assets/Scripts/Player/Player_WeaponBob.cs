using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WeaponBob : MonoBehaviour
{
    public Player_Movement player_Movement;
    private Animator animator;

    public AudioSource footSteps;

    private bool shouldBob = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if(!shouldBob) return;
        animator.SetBool("IsMoving" , player_Movement.isMoving);
    }

    public void Set_shouldBob(bool b)
    {
        shouldBob = b;
    }

    public void PlayFootSteps()
    {
        footSteps.Play();
    }

}
