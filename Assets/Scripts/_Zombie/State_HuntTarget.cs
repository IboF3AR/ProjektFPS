using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_HuntTarget : State
{
    public State attackState;
    Transform target;
    private bool toAttack = false;


    override public void OnStateEnter()
    {
        if(target == null)
            target = manager.detection.foundTargetTransform;
        manager.locomotion.StartFollowTarget(target);
    }

    public override bool TickAndShouldSwitch()
    {
        // Reached Patroulpoint
        if(manager.locomotion.GetReached())
        {
            manager.locomotion.StopMovement();
            toAttack = true;
            return true;
        }
        return false;
    }

    public override State GetStateToSwitchTo()
    {
        if(toAttack)
        {
            toAttack = false;
            return attackState;
        }
        return null;
    }

}
