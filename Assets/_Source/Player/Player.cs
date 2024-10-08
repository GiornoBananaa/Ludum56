using System;
using System.Collections;
using System.Collections.Generic;
using EntitySystem.CombatSystem;
using UnityEngine;
using UnityEngine.UI;

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

        if (hp < 1)
        {
            hpImage1.enabled = false;
        }
        else
        {
            hpImage1.enabled = true;
        }
    }
}
