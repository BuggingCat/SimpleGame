using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Large : MonoBehaviour
{
    private Entity entity;
    private CharacterStats myStats;
    private Slider slider;
    private void Start()
    {
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        myStats = GetComponentInParent<CharacterStats>();

    }

    private void Update()
    {
        UpdatsHealthUI();
    }

    private void UpdatsHealthUI()
    {
        slider.maxValue = myStats.maxHealth.GetValue();
        slider.value = myStats.currentHealth;
    }
}
