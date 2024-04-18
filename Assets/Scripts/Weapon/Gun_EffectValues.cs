using UnityEngine;

[System.Serializable]
public struct Gun_EffectValues
{
    [Header("Recoil Values")]
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    public float snappiness;
    public float returnSpeed;

    [Header("Camera Shake")]
    public Vector3 Amount;/// Amount of Shake
    public float Duration; /// Duration of Shake
    public float Speed; /// Shake Speed
    public AnimationCurve Curve;
    public bool DeltaMovement;
}
