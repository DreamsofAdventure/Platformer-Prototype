using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;

    void Start()
    {
        staminaSlider = GetComponent<Slider>();
    }

    public void SetMaxStamina(float maxStamina){
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }

    public void SetStamina(float stamina){
        staminaSlider.value = stamina;
    }
}
