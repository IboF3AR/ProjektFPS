using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    public Player_Values player_Values;
    public TMP_Text subtitle;
    public Slider healthSlider;
    public Gradient healthBar_gradient;

    public Image healthbar_Image;

    private void Start()
    {
        subtitle = GameObject.Find("Subtitle").GetComponent<TMP_Text>();
        subtitle.text = "";
        
        healthbar_Image.color = healthBar_gradient.Evaluate(1f);
    } 

    public void SetSubtitle(string text)
    {
        subtitle.text = text;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        healthbar_Image.color = healthBar_gradient.Evaluate(healthSlider.normalizedValue);
    }

    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
    }
}
