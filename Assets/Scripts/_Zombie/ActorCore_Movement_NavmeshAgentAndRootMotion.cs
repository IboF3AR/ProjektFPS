using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using UnityEngine;
using UnityEngine.AI;

public class ActorCore_Movement_NavmeshAgentAndRootMotion : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private BaseEnemy_Manager manager;
    private Animator animator;
    private enum LocomotionState
    {
        Idle, Turning, Moving, Reached
    }

    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private LocomotionState locomotionState;
    [HideInInspector] public float distanceToTarget;
    private Vector3 targetPosition;
    private Transform target;
    private bool reached = false;
    private float stoppingDistance = 0.3f;



    private void Start()
    {
        manager = GetComponent<BaseEnemy_Manager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        animator = GetComponent<BaseEnemy_Manager>().animator;
        stoppingDistance = navMeshAgent.stoppingDistance + 0.2f;
    }

    public void Update()
    {
        if (locomotionState == LocomotionState.Moving)
        {
            CheckReachedDestination();
            if (target != null)
                UpdateTargetPosition();
            MoveForwardViaRootMotion();
            RotateTowardsTarget();
            navMeshAgent.transform.localPosition = Vector3.zero;
        }
    }

    public void StartMoveToPoint(Vector3 position)
    {
        targetPosition = position;
        animator.CrossFade("Running", 0.2f);
        locomotionState = LocomotionState.Moving;
        //navMeshAgent.SetDestination(targetPosition);
    }

    public void StartFollowTarget(Transform target)
    {
        this.target = target;
        animator.CrossFade("Running", 0.2f);
        locomotionState = LocomotionState.Moving;
    }

    private void MoveForwardViaRootMotion()
    {

    }



    private void RotateTowardsTarget()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(target.position);
        manager.transform.rotation = Quaternion.Slerp(
        manager.transform.rotation,
        navMeshAgent.transform.rotation,
        rotationSpeed / Time.deltaTime);
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
        if (reached)
        {
            reached = false;
            return true;
        }
        return false;
    }

    private void UpdateTargetPosition()
    {
        //navMeshAgent.SetDestination(target.position);
    }

    public void StopMovement()
    {
        animator.CrossFade("Idle", 0.2f);
        locomotionState = LocomotionState.Idle;
        navMeshAgent.SetDestination(transform.position);
    }
}


