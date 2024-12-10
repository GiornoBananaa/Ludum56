using System.Linq;
using UnityEngine;

namespace EntitySystem.CombatSystem
{
    public class EntityCombat : IEntityCombat
    {
        public void HandleCombat(Entity entity)
        {
            CheckRange(entity);
        }
        
        private void CheckRange(Entity entity)
        {
            Collider2D collider = Physics2D.OverlapCircle(entity.transform.position, 
                entity.MaxAttackRange, entity.Stats.AttackTargetLayer);
            
            if(collider != null && collider.transform.TryGetComponent(out IDamageable target))
            {
                EntityAttack entityAttack = ChooseRandomAttack(entity, collider.transform, out int attackIndex);
                if(entityAttack == null) return;
                entityAttack.Attack(collider.transform, target);
                entity.AnimationHandler.SetLongAttack(entityAttack.Stats.LongAttack);
                entity.AnimationHandler.PlayAttack(attackIndex);
                entity.SoundPlayer.PlayAttack(attackIndex);
            }
        }
        
        private EntityAttack ChooseRandomAttack(Entity entity, Transform target, out int attackIndex)
        {
            int weightSum = entity.Attacks
                .Where(entityEnemyAttack => entityEnemyAttack.CanAttack && Vector2.Distance(entity.transform.position,target.transform.position) <= entityEnemyAttack.Stats.AttackRange)
                .Sum(entityEnemyAttack => entityEnemyAttack.Stats.Weight);
            EntityAttack attack = null;
            int rnd = Random.Range(0, weightSum+1);
            int buf = 0;
            int index = -1;
            attackIndex = 0;
            foreach (var entityEnemyAttack in entity.Attacks)
            {
                index++;
                if (Vector2.Distance(entity.transform.position, target.transform.position) >
                    entityEnemyAttack.Stats.AttackRange || !entityEnemyAttack.CanAttack)
                    continue;
                buf += entityEnemyAttack.Stats.Weight;
                if (buf >= rnd)
                {
                    attackIndex = index;
                    attack = entityEnemyAttack;
                }
                if (entityEnemyAttack.IsAttacking)
                    return null;
            }

            return attack;
        }
    }
}