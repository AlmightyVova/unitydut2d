using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public HealthSystem playerHealth = new HealthSystem(100);
    private Animator animator;
    private PlayerMoveController pmc;
    public HealthBar healthBar;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        pmc = gameObject.GetComponent<PlayerMoveController>();
        
        healthBar.SetMaxHealth(playerHealth.GetMaxHealth());

        healthBar.SetMaxHealth(playerHealth.GetMaxHealth());
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("chest"))
        {
            playerHealth.Damage(playerHealth.GetHealth());
            healthBar.SetHealth(playerHealth.GetHealth());
        }
    }

    void Update()
    {
        if (playerHealth.GetHealth() == 0)
        {
            animator.Play("Dying");
            pmc.isDead = true;
        }
    }
}