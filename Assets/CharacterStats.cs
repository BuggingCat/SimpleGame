using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat strength;
    public Stat damage;
    public Stat fall_damage; // À§À¿…À∫¶
    public Stat maxHealth;

    public int currentHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = damage.GetValue() + strength.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void DoFallDamage(CharacterStats _targetStats)
    {
        int totalDamage = fall_damage.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth = currentHealth - _damage;
        
        if (currentHealth < 0)
            Die();
    }

    public virtual void TakeFallDamage(int _falldamage)
    {
        currentHealth = currentHealth - _falldamage;

        if (currentHealth < 0)
            Die();
    }

    public virtual void Die()
    {
        
    }
}
