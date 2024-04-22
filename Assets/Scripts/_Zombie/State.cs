using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    protected BaseEnemy_Manager manager;

    public void SetManager(BaseEnemy_Manager manager)
    {
        this.manager = manager;
    }

    public virtual void OnStateEnter()
    {
        Debug.Log("Entered: " + this.name);
    }

    public virtual bool TickAndShouldSwitch()
    {
        return false;
    }

    public virtual State GetStateToSwitchTo()
    {
        return null;    
    }
}
