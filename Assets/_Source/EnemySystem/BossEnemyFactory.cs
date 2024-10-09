using AudioSystem;
using Core.DataLoadingSystem;
using EntitySystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using UnityEngine;

namespace EnemySystem
{
    public class BossEnemyFactory : EnemyFactory
    {
        public override EntityType EnemyType => EntityType.BossEnemy;


        public BossEnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer, AudioVolumeSetter audioVolumeSetter) : base(dataRepository, enemyDeathHandler, enemyMovement, entityCombat, poolsContainer, audioVolumeSetter)
        {
        }
    }
}