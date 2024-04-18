using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Values : MonoBehaviour
{
    public int maxHp;
    public int currentHP;

    public bool isAlive = true;

    private void Start()
    {
        currentHP = maxHp;
    }

    public void TakeDamage(int damage)
    {
        if(!isAlive) return;
        currentHP -= damage;
        if(currentHP <= 0)
        {
            isAlive = false;
            GetComponent<AI_AnimationController>().TriggerAnimation("death");
            GetComponentInChildren<Animator>().SetBool("IsDead", true);
            DisableAll();
            this.enabled = false;
        }
    }

    private void DisableAll()
    {
        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour c in comps)
        {
            c.enabled = false;
        }

        MonoBehaviour[] children_comps = GetComponentsInChildren<MonoBehaviour>();
        foreach(MonoBehaviour c in children_comps)
        {
            c.enabled = false;
        }

        Collider[] colliders = GetComponents<Collider>();
        foreach(Collider c in colliders)
        {
            c.enabled = false;
        }

        Collider[] children_colliders = GetComponentsInChildren<Collider>();
        foreach(Collider c in children_colliders)
        {
            c.enabled = false;
        }
    }
}
