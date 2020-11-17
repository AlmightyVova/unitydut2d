using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int _health;
    private int _shield;

    public int Health => _health;
    public int Shield => _shield;

    public HealthSystem()
    {
        _health = 1;
        _shield = 0;
    }

    public void Damage()
    {
        if (_shield == 0)
        {
            _health = 0;
            if (_health < 0) _health = 0;
        }
        else
        {
            _shield = 0;
        }
    }

    public void Kill()
    {
        _shield = 0;
        _health = 0;
    }

    public void HealShield()
    {
        if (_shield == 0)
        {
            _shield = 1;
        }
    }
}