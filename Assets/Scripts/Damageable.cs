using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damageable : MonoBehaviour
{
    private Character_Values character_Values;
    public event Action<int> e_takeDamage;

    public GameObject effectOnHit;

    public int multyPlier  = 1;

    public void Awake()
    {
        character_Values = GetComponentInParent<Character_Values>();
    }

    public void TakeDamage(int damage, Vector3 pointOfDamage, Vector3 normal)
    {
        PlayEffects(pointOfDamage, normal);
        character_Values.TakeDamage((damage *multyPlier));
        e_takeDamage?.Invoke((damage *multyPlier));
        Debug.Log("transform name: " + transform.name);
    }

    private void PlayEffects(Vector3 pointOfDamage, Vector3 normal)
    {
        ParticleSystem particleSystem = effectOnHit.GetComponentInChildren<ParticleSystem>();
        particleSystem.transform.position = pointOfDamage;
        particleSystem.transform.rotation = Quaternion.LookRotation(normal);
        if(particleSystem != null)
        {
            particleSystem.Play();
        }
    }
}
