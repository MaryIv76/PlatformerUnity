using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private int health = 100;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    private void Start()
    {
        SetMaxHealth(health);
    }

    private void Update()
    {
        SetHealth(health);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
