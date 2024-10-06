using System;
using EntitySystem.CombatSystem;

namespace EntitySystem
{
    public class Health : IDamageable
    {
        public readonly int MaxHP;
        public readonly Entity Entity;
        public int HP { get; private set; }
        
        public Action<int> OnTakeDamage;
        public Action<Entity> OnDeath;
        
        public Health(Entity entity, int hp)
        {
            Entity = entity;
            MaxHP = hp;
            HP = hp;
        }
        
        public void TakeDamage(int damage)
        {
            if(damage == 0 || HP <= 0) return;

            HP -= damage;
            OnTakeDamage?.Invoke(damage);
            
            if(HP<=0)
                OnDeath?.Invoke(Entity);
        }

        public void Reset()
        {
            HP = MaxHP;
        }
    }
}