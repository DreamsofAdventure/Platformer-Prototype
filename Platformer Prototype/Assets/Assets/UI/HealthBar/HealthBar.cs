using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpSlider;

    void Start()
    {
        hpSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth){
        hpSlider.maxValue = maxHealth;
        hpSlider.value = maxHealth;
    }

    public void SetHealth(int health){
        hpSlider.value = health;
    }
}
