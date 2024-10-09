using AudioSystem;
using Core.DataLoadingSystem;
using EntitySystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using UnityEngine;

namespace EnemySystem
{
    public class ProjectileEnemyFactory : EnemyFactory
    {
        public override EntityType EnemyType => EntityType.ProjectileEnemy1;


        public ProjectileEnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer, AudioVolumeSetter audioVolumeSetter) : base(dataRepository, enemyDeathHandler, enemyMovement, entityCombat, poolsContainer, audioVolumeSetter)
        {
        }
    }
    
    public class SecondProjectileEnemyFactory : EnemyFactory
    {
        public override EntityType EnemyType => EntityType.ProjectileEnemy2;


        public SecondProjectileEnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer, AudioVolumeSetter audioVolumeSetter) : base(dataRepository, enemyDeathHandler, enemyMovement, entityCombat, poolsContainer, audioVolumeSetter)
        {
        }
    }
}