﻿using System.Collections.Generic;
using AudioSystem;
using Core;
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
        private readonly AudioVolumeSetter _audioVolumeSetter;
        
        public EnemyFactory(IRepository<ScriptableObject> dataRepository, EnemyDeathHandler enemyDeathHandler, 
            IEntityMovement enemyMovement, IEntityCombat entityCombat, EnemyPoolsContainer poolsContainer, AudioVolumeSetter audioVolumeSetter)
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
            _audioVolumeSetter = audioVolumeSetter;
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
            enemy.Construct(_enemyMovement, _entityCombat, new DamageDealer(), _entityStats, _audioVolumeSetter);
            _enemyDeathHandler.Subscribe(enemy);
            return enemy;
        }
    }
}