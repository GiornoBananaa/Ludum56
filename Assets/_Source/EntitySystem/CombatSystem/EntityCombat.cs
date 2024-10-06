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
            if(collider != null && collider.transform.TryGetComponent(out Entity target))
                ChooseRandomAttack(entity, target).Attack(target);
        }
        
        private EntityAttack ChooseRandomAttack(Entity entity, Entity target)
        {
            int weightSum = entity.EntityAttacks
                .Where(entityEnemyAttack => entityEnemyAttack.CanAttack && Vector2.Distance(entity.transform.position,target.transform.position) <= entityEnemyAttack.Stats.AttackRange)
                .Sum(entityEnemyAttack => entityEnemyAttack.Stats.Weight);
            
            int rnd = Random.Range(0, weightSum+1);
            int buf = 0;
            
            foreach (var entityEnemyAttack in entity.EntityAttacks)
            {
                if (Vector2.Distance(entity.transform.position, target.transform.position) >
                    entityEnemyAttack.Stats.AttackRange || !entityEnemyAttack.CanAttack)
                    continue;
                buf += entityEnemyAttack.Stats.Weight;
                if (rnd >= buf)
                {
                    return entityEnemyAttack;
                }
            }

            return entity.EntityAttacks[0];
        }
    }
}