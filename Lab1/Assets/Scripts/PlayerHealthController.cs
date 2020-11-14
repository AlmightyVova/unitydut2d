using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public HealthSystem healthSystem = new HealthSystem();
    private Animator animator;
    private PlayerMoveController pmc;
    public HealthBar healthBar;
    public PotionShieldBar potionBar;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        animator = gameObject.GetComponent<Animator>();
        pmc = gameObject.GetComponent<PlayerMoveController>();

        healthBar.SetMaxHealth(1);
        potionBar.SetMaxShield(1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("chest"))
        {
            healthSystem.Damage();
            healthBar.SetHealth(healthSystem.Health);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Potion"))
        {
            healthSystem.HealShield();
            potionBar.SetShield(healthSystem.Shield);
        }
    }

    void Update()
    {
        if (healthSystem.Health == 0)
        {
            animator.Play("Dying");
            pmc.isDead = true;
        }
    }
}