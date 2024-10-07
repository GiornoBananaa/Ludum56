using System;
using EntitySystem;
using UnityEngine;

namespace EnemySystem
{
    public class Enemy : Entity
    {
        [field: SerializeField] protected bool kill;

        private void FixedUpdate()
        {
            if(kill && Health.HP > 0)
                Health.TakeDamage(100);
        }
    }
}
