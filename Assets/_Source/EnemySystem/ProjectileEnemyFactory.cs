using Core.DataLoadingSystem;
using EntitySystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using UnityEngine;

namespace EnemySystem
{
    public class ProjectileEnemyFactory : EnemyFactory
    {
        public override EntityType EnemyType => EntityType.ProjectileEnemy;


        public ProjectileEnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer) : base(dataRepository, enemyDeathHandler, enemyMovement, entityCombat, poolsContainer)
        {
        }
    }
}