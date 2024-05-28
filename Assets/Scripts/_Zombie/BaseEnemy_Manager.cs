using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class BaseEnemy_Manager : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private State currentState;
    [Header("Core")]
    [HideInInspector] public ActorCore_Detection detection;
    [HideInInspector] public ActorCore_Movement_NavmeshAgentAndRootMotion locomotion;
    [HideInInspector] public Animator animator;

    void Awake()
    {
        SetComponents();
        currentState = startingState;
        State[] states = GetComponents<State>();
        foreach (var item in states)
        {
            item.SetManager(this);
        }
    }

    void SetComponents()
    {
        detection = GetComponent<ActorCore_Detection>();
        animator = GetComponent<Animator>();
        locomotion = GetComponent<ActorCore_Movement_NavmeshAgentAndRootMotion>();
    }

    void FixedUpdate()
    {   
        HandleStates();
    }

    void HandleStates()
    {
        if(currentState == null) Debug.Log("<color=red> currneState should never be null </color>");
        if(currentState.TickAndShouldSwitch())
        {
            currentState = currentState.GetStateToSwitchTo();
            currentState.OnStateEnter();
        }
    }

}
