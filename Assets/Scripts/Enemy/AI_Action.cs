using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_Action : MonoBehaviour
{
    protected Animator animator;
    public bool canBeInterrupted = false; //info: except death

    public bool animationIsPlaying;

    public abstract void PerformAction();

    public virtual void Tick(){}
}
