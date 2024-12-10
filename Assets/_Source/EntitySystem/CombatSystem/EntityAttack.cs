using UnityEngine;

namespace EntitySystem.CombatSystem
{
    public abstract class EntityAttack : MonoBehaviour
    {
        protected Entity Entity;
        public AttackStat Stats { get; private set; }
        public virtual bool CanAttack { get; protected set; } = true;
        public abstract bool IsAttacking { get; }

        public void SetConfig(Entity entity, AttackStat stats)
        {
            Entity = entity;
            Stats = stats;
        }
        
        public abstract void Attack(Transform targetTransform, IDamageable target);
    }
}