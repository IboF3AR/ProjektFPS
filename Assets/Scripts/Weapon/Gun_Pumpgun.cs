using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Pumpgun : Gun
{
    [Header("PumpGun")]
    public int numberOfBulletsPerShot = 4;
    public float inacuracyDistance = 5f;


    private void Awake()
    {
        SetUp();
    }

    public override void Shoot()
    {
        PrepareShooting();
        RaycastHit hit;
        for (int i = 0; i < numberOfBulletsPerShot; i++)
        {
            if(Physics.Raycast(fpsCam.transform.position, (GetShootingDirection() + newFPSCamForward), out hit, maxRange, ~ignoreLayer))
            {
                currentDamageTodeal = GetDamageBasedOnRange(Vector3.Distance(fpsCam.transform.position, hit.point));
                Test_CreateVisuals(hit.point);
                DoAction(hit.transform.gameObject, hit.point, hit.normal);
                Effect_OnShoot(hit);
            }
        }         
    }

    private Vector3 GetShootingDirection()
    {
        Vector3 targetPos = fpsCam.transform.position + fpsCam.transform.forward * maxRange;
        targetPos = new Vector3(
            targetPos.x + Random.Range(-inacuracyDistance, inacuracyDistance),
            targetPos.y + Random.Range(-inacuracyDistance, inacuracyDistance),
            targetPos.z + Random.Range(-inacuracyDistance, inacuracyDistance)
        );

        Vector3 direction = targetPos - fpsCam.transform.position;
        return direction.normalized;
    }
}
