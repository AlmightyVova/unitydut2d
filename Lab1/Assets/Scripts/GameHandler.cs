using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private void Start()
    {
        HealthSystem healthSystem = new HealthSystem(200);
        print($"Health: {healthSystem.GetHealth()}");
        healthSystem.Damage(13);
        print($"Health: {healthSystem.GetHealth()}");
        
        
    }
}