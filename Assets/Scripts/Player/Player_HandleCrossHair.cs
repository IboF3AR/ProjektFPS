using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HandleCrossHair : MonoBehaviour
{
    // Visuals
    public string CrossHairName = "CrossHair";
    [HideInInspector] public RectTransform crossHair;
    float maxSizeForUI = 110f;
    float currentSizeValue = 0f;
    float sizeGrowthSpeed;
    [HideInInspector] public float calmDuration;
    private bool crossHairIsPerfect = true;

    public void SetUp(float calmDuration)
    {
        this.calmDuration = calmDuration;
        crossHair = GameObject.Find(CrossHairName).GetComponent<RectTransform>();
        sizeGrowthSpeed = (maxSizeForUI - 25f) / calmDuration; //info: 25 is the normal size
    }

    // Update is called once per frame
    public void Tick()
    {
        if(!crossHairIsPerfect)
        {
            currentSizeValue -= Time.deltaTime * sizeGrowthSpeed; 
            crossHair.sizeDelta = new Vector2(currentSizeValue, currentSizeValue);
            if(currentSizeValue <= 25f)
            {
                currentSizeValue = 25f;
                crossHairIsPerfect = true;
            }
        }
    }

    public void DecreasePrecision()
    {
        crossHair.sizeDelta = new Vector2(maxSizeForUI, maxSizeForUI);
        currentSizeValue = maxSizeForUI;
        crossHairIsPerfect = false;
    }
}
