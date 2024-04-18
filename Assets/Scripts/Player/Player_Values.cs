using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Values : MonoBehaviour
{
    private Player_UI playerUI;
    public int maxHp;
    public int currentHP;

    private void Start()
    {
        currentHP = maxHp;
        
        playerUI = GetComponent<Player_UI>();
        playerUI.SetMaxHealth(maxHp);
        playerUI.SetHealth(maxHp);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            TEST_takeDamage();
        }
    }

    private void TEST_takeDamage()
    {
        TakeDamage(10);
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("Damage taken: " + damage);
        playerUI.SetHealth(currentHP);
        if(currentHP <= 0)
        {
            Debug.Log("<color=red> YOU DIED </color>");
        }
    }

    public void Heal(int heal)
    {
        currentHP += heal;
        if(currentHP > maxHp)
        {
            currentHP = maxHp;
        }
        playerUI.SetHealth(currentHP);
    }
}
