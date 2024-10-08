using System;
using System.Collections;
using System.Collections.Generic;
using EntitySystem.CombatSystem;
using UnityEngine;
using UnityEngine.UI; // Add this namespace for UI components

public class Player : MonoBehaviour, IDamageable
{
    [field: SerializeField] protected bool kill;

    public int attackPower = 1;
    public int passiveAttack = 0;
    public int AttackJerk = 1;

    public int hp = 1;
    public float attackRadius = 5f;

    private bool IsDead;
    public Action OnDeath;

    // Add references to the HP images
    public Image hpImage1;
    public Image hpImage2;

    private void FixedUpdate()
    {
        if (kill && hp > 0)
            TakeDamage(100);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (!IsDead && hp <= 0)
        {
            IsDead = true;
            OnDeath?.Invoke();
        }

        // Update the HP images based on the player's HP
        if (hp < 1)
        {
            hpImage1.enabled = false; // Hide the first image
        }
        else
        {
            hpImage1.enabled = true; // Show the first image
        }
    }
}
