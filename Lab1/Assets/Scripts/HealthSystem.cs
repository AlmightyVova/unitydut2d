using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private readonly int maxHealth;
    private int health;

    public HealthSystem(int health)
    {
        this.maxHealth = health;
        this.health = maxHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth) health = maxHealth;
    }
}