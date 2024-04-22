using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActorCore_Movement_NavmeshAgentAndRootMotion : MonoBehaviour
{
    private enum LocomotionState
    {
        Idle, Turning, Moving, Reached
    }

    [SerializeField] private LocomotionState locomotionState;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;
    [HideInInspector] public float distanceToTarget;
    private Animator animator;
    private Vector3 targetPosition;
    private Transform target;
    private bool reached = false;
    private float stoppingDistance = 0.3f;
    private NavMeshAgent navMeshAgent;



    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        stoppingDistance = navMeshAgent.stoppingDistance + 0.2f;
    }

    public void Update()
    {
        if(locomotionState == LocomotionState.Moving)
        {
            CheckReachedDestination();
            if(target != null)
                UpdateTargetPosition();
        }
    }

    public void StartMoveToPoint(Vector3 position)
    {
        targetPosition = position;
        animator.CrossFade("Running", 0.2f);
        locomotionState = LocomotionState.Moving;
        navMeshAgent.SetDestination(targetPosition);
    }

    public void StartFollowTarget(Transform target)
    {
        this.target = target;
        animator.CrossFade("Running", 0.2f);
        locomotionState = LocomotionState.Moving;
    }
 
    private void CheckReachedDestination()
    {
        distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget <= stoppingDistance)
        {
            locomotionState = LocomotionState.Reached;
            reached = true;
        }
    }

    public bool GetReached()
    {
        if(reached)
        {
            reached = false;
            return true;
        }
        return false;
    }

    private void UpdateTargetPosition()
    {
        navMeshAgent.SetDestination(target.position);
    } 

    public void StopMovement()
    {
        animator.CrossFade("Idle", 0.2f);
        locomotionState = LocomotionState.Idle;
        navMeshAgent.SetDestination(transform.position);
    }
}


