using System;
using EntitySystem.CombatSystem;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [field: SerializeField] protected bool kill;

    public int killedEnemies;

    public int attackPower = 1;
    public int passiveAttack = 0;
    public int AttackJerk = 1;

    public int hp = 1;
    public float speed = 5f;
    public float skillSpeed = 8f;
    public float attackRadius = 5f;
    public float attackAngle = 120f;
    public LayerMask enemyLayer;

    private bool IsDead;

    public Action OnDeath;
    public Action OnLevelSwitch;
    public Action OnEnemyKilled;

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

    public void EnemyKilled()
    {
        killedEnemies++;
        OnEnemyKilled?.Invoke();
    }
}
