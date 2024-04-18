using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    private NavMeshAgent agent;
    public float speed;
    public float stopDistance = 1f;
    Vector3 point_destination;
    [Header("Follow")]
    public Transform target;
    public bool hasTarget;
    public bool isMoving;
    public bool IsBlockedByAction = false;
    public bool isInRange;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = transform.position;
        agent.speed = speed;
        agent.stoppingDistance = stopDistance;
    }

    // Update is called once per frame
    public void Tick()
    {
        if(hasTarget)
        {
            isInRange  = CheckIsInRange();
            FollowTarget();
        }
        isMoving = (agent.remainingDistance > stopDistance);
    }

    public void MoveTo(Vector3 pointDestination)
    {
        if(!IsBlockedByAction)
        point_destination = pointDestination;
    }

    #region Follow
    public void StartFollowTarget(Transform target)
    {
        this.target = target;
        FollowTarget();
    }

    public void StopFollowTarget()
    {
        target = null;
        agent.updateRotation = true;
    }

    private bool CheckIsInRange()
    {
        return (Vector3.Distance(transform.position, target.position) <= stopDistance);
    }

    private void FollowTarget()
    { 
        if(IsBlockedByAction)
        {
            return;
        }

        if(isInRange)
        {
            agent.updateRotation = false;
            RotateToTarget();
        }
        else
        {
            agent.updateRotation = true;
        }

        if(!isInRange)
        {
            agent.destination = target.position;
        }
    }


    private void RotateToTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f);
    }
    #endregion

    public void Set_IsBlockedByAction(bool b)
    {
        //info: use this for attacking
        agent.isStopped = b;
        IsBlockedByAction = b;
    }
}
