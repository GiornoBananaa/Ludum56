using AudioSystem;
using Core.DataLoadingSystem;
using EntitySystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using UnityEngine;

namespace EnemySystem
{
    public class MeleeEnemyFactory : EnemyFactory
    {
        public override EntityType EnemyType => EntityType.MeleeEnemy1;


        public MeleeEnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer, AudioVolumeSetter audioVolumeSetter) : base(dataRepository, enemyDeathHandler, enemyMovement, entityCombat, poolsContainer, audioVolumeSetter)
        {
        }
    }
    
    public class SecondMeleeEnemyFactory : EnemyFactory
    {
        public override EntityType EnemyType => EntityType.MeleeEnemy2;


        public SecondMeleeEnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer, AudioVolumeSetter audioVolumeSetter) : base(dataRepository, enemyDeathHandler, enemyMovement, entityCombat, poolsContainer, audioVolumeSetter)
        {
        }
    }
}