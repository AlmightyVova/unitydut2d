using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private readonly int maxHealth;
    private int health;
    private int maxShield;
    private int shield;

    public int Health => health;
    public int Shield => shield;

    public HealthSystem()
    {
        maxHealth = 1;
        health = maxHealth;
        maxShield = 1;
        shield = 0;
    }

    public void Damage()
    {
        if (shield == 0)
        {
            health = 0;
            if (health < 0) health = 0;
        }
        else
        {
            shield = 0;
        }
    }

    public void HealShield()
    {
        if (shield == 0)
        {
            shield = 1;
        }
    }
}