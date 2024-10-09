using AudioSystem;
using Core.DataLoadingSystem;
using EntitySystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using UnityEngine;

namespace EnemySystem
{
    public class RedProjectileEnemyFactory : EnemyFactory
    {
        public override EntityType EnemyType => EntityType.RedProjectileEnemy;


        public RedProjectileEnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer, AudioVolumeSetter audioVolumeSetter) : base(dataRepository, enemyDeathHandler, enemyMovement, entityCombat, poolsContainer, audioVolumeSetter)
        {
        }
    }
    
    public class BlueProjectileEnemyFactory : EnemyFactory
    {
        public override EntityType EnemyType => EntityType.BlueProjectileEnemy;


        public BlueProjectileEnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer, AudioVolumeSetter audioVolumeSetter) : base(dataRepository, enemyDeathHandler, enemyMovement, entityCombat, poolsContainer, audioVolumeSetter)
        {
        }
    }
}