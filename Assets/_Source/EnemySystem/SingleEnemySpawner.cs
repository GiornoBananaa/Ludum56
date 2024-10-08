using System.Collections.Generic;
using EntitySystem;
using UnityEngine;
using UnityEngine.Events;
using VContainer;

namespace EnemySystem
{
    public class SingleEnemySpawner : EntitySpawner
    {
        [SerializeField] private bool _launchOnStart;
        [SerializeField] private EntityType _entityType;
        
        private EnemyPoolsContainer _enemyPoolsContainer;
        private bool _isSpawned;

        public override bool AllEntitiesKilled { get; protected set; }

        public override int EntitiesKilledCount => AllEntitiesKilled ? 1 : 0;
        
        [field: SerializeField] public override UnityEvent OnAllEntitiesKilled { get; protected set; }
        [field: SerializeField] public override UnityEvent OnAllEnemiesSpawned { get; protected set; }

        [Inject]
        public void Construct(EnemyPoolsContainer enemyPoolsContainer)
        {
            Debug.Log("Construct");
            _enemyPoolsContainer = enemyPoolsContainer;
        }

        private void Start()
        {
            if(_launchOnStart)
                LaunchSpawner();
        }
        
        public override void LaunchSpawner()
        {
            if(_isSpawned) return;
            _isSpawned = true;
            SpawnEnemy(_entityType);
            
            OnAllEnemiesSpawned?.Invoke();
        }

        private void OnDeath(Entity entity)
        {
            entity.Health.OnDeath -= OnDeath;
            AllEntitiesKilled = true;
            OnAllEntitiesKilled?.Invoke();
        }

        private void SpawnEnemy(EntityType enemyType)
        {
            Entity enemy = _enemyPoolsContainer.Get(enemyType);
            enemy.transform.position = transform.position;
            enemy.gameObject.SetActive(true);
            enemy.Health.OnDeath += OnDeath;
        }
    }
}