using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WeaponRecoil : MonoBehaviour
{

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    [SerializeField] private float recoilX = -2f;
    [SerializeField] private float recoilY = 2f;
    [SerializeField] private float recoilZ = 0.35f;

    // Settings
    [SerializeField] private float snappiness = 6;
    [SerializeField] private float returnSpeed = 2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void SetRecoilValues(Gun_EffectValues gunEffectValues)
    {
        recoilX         = gunEffectValues.recoilX;
        recoilY         = gunEffectValues.recoilY;
        recoilZ         = gunEffectValues.recoilZ;
        snappiness      = gunEffectValues.snappiness;
        returnSpeed     = gunEffectValues.returnSpeed;
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ)); 
    }
}

