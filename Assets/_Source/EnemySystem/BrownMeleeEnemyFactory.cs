using AudioSystem;
using Core.DataLoadingSystem;
using EntitySystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using UnityEngine;

namespace EnemySystem
{
    public class BrownMeleeEnemyFactory : EnemyFactory
    {
        public override EntityType EnemyType => EntityType.BrownMeleeEnemy;


        public BrownMeleeEnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer, AudioVolumeSetter audioVolumeSetter) : base(dataRepository, enemyDeathHandler, enemyMovement, entityCombat, poolsContainer, audioVolumeSetter)
        {
        }
    }
    
    public class PurpleMeleeEnemyFactory : EnemyFactory
    {
        public override EntityType EnemyType => EntityType.PurpleMeleeEnemy;


        public PurpleMeleeEnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer, AudioVolumeSetter audioVolumeSetter) : base(dataRepository, enemyDeathHandler, enemyMovement, entityCombat, poolsContainer, audioVolumeSetter)
        {
        }
    }
}