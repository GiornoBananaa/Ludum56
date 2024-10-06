using System.Collections;
using System.Collections.Generic;
using EntitySystem.CombatSystem;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int attackPower = 1;
    public int hp = 1;
    public float attackRadius = 5f;


    public void TakeDamage(int damage)
    {
        hp -= damage;
    }
}