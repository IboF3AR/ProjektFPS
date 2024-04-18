using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Pistole : Gun
{

    private void Awake()
    {
        SetUp();
    }

    public override void Shoot()
    {
        PrepareShooting();
        RaycastHit hit; 
        if(Physics.Raycast(fpsCam.transform.position, newFPSCamForward, out hit, maxRange, ~ignoreLayer))
        {
            currentDamageTodeal = GetDamageBasedOnRange(Vector3.Distance(fpsCam.transform.position, hit.point));
            Test_CreateVisuals(hit.point);
            DoAction(hit.transform.gameObject, hit.point, hit.normal);
            Effect_OnShoot(hit);
        }
    }
}
