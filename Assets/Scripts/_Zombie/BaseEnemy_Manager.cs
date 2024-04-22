using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class BaseEnemy_Manager : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private State currentState;
    [Header("Core")]
    public ActorCore_Detection detection;
    public ActorCore_Movement_NavmeshAgent locomotion;
    public Animator animator;

    void Start()
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
        animator = GetComponentInChildren<Animator>();
        locomotion = GetComponent<ActorCore_Movement_NavmeshAgent>();
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
        }
    }

}
