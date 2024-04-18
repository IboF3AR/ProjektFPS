using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    Player_DoAction player_DoAction;
    Player_Values player_Values;
    Player_UI player_UI;

    private void Start()
    {
        player_Values = GetComponent<Player_Values>();
        player_DoAction = GetComponent<Player_DoAction>();
        player_UI = GetComponent<Player_UI>();
    }

    private void Update()
    {
        if(player_DoAction.hasInteractableTarget)
        {
            SetSubtitle(player_DoAction.interaction_Target.GetDescription());
        }
        else 
        {
            SetSubtitle("");
        }
    }

    private void SetSubtitle(string text)
    {
        player_UI.SetSubtitle(text);
    }

    public void TakeDamage(int damage)
    {
        player_Values.TakeDamage(damage);
    }
}
