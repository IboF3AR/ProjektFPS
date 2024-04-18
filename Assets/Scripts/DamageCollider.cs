using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// info: Use this for Enemys
public class DamageCollider : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        Player_Interaction damageable = other.GetComponent<Player_Interaction>();
        if(damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
