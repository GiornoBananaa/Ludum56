using System.Collections.Generic;
using Core.DataLoadingSystem;
using EntitySystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using UnityEngine;

namespace EnemySystem
{
    public abstract class EnemyFactory : IFactory<Enemy>
    {
        public abstract EntityType EnemyType { get; }

        private readonly Enemy _prefab;
        private readonly DamageDealer _damageDealer;
        private readonly EntityStats _entityStats;
        private readonly EnemyDeathHandler _enemyDeathHandler;
        private readonly IEntityMovement _enemyMovement;
        private readonly IEntityCombat _entityCombat;
        private readonly EnemyPoolsContainer _enemyPoolsContainer;
        
        public EnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, 
            IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer)
        {
            EntityDataSO enemyData = null;
            List<EntityDataSO> repository = dataRepository.GetItem<EntityDataSO>();
            foreach (var entityData in repository)
            {
                if (entityData.EntityStats.EntityType != EnemyType) continue;
                enemyData = entityData;
                break;
            }
            
            if (enemyData == null)
                enemyData = repository[0];
            
            _enemyDeathHandler = enemyDeathHandler;
            _prefab = enemyData.Prefab;
            _entityStats = enemyData.EntityStats;
            _enemyMovement = enemyMovement;
            _entityCombat = entityCombat;
            _enemyPoolsContainer = poolsContainer;
            poolsContainer.AddPool(EnemyType, Create);
        }
        
        public virtual Enemy Create()
        {
            return InstantiateAndConstruct();
        }

        protected Enemy InstantiateAndConstruct()
        {
            Enemy enemy = Object.Instantiate(_prefab);
            enemy.gameObject.SetActive(false);
            enemy.Construct(_enemyMovement, _entityCombat, new DamageDealer(), _entityStats);
            _enemyDeathHandler.Subscribe(enemy);
            enemy.Health.OnDeath += e => _enemyPoolsContainer.Return(e);
            return enemy;
        }
    }
}