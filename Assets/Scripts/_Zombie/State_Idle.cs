using UnityEngine;
public class State_Idle : State
{
    public State huntTargetState;
    private bool toHuntTarget;

    public override bool TickAndShouldSwitch()
    {
        if(manager.detection.foundTarget)
        {
            toHuntTarget = true;
            return true;
        }
        return false;
    }

    public override State GetStateToSwitchTo()
    {
        if(toHuntTarget)
        {
            toHuntTarget = false;
            return huntTargetState;
        }
        return null; //
    }
}