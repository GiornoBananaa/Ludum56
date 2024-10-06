using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using EntitySystem;
using UnityEngine;
using UnityEngine.Events;
using VContainer;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnerConfigSO SpawnerConfig;
        
        private HashSet<Entity> _spawnedEnemies;
        private EnemyPoolsContainer _enemyPoolsContainer;
        private CancellationTokenSource _spawnerLoopCancellation;
        private Transform _player;
        private bool _isSpawning;
        private float _weightSum;
        private int _entitiesSpawned;
        private int _entitiesKilled;
        
        public UnityEvent OnAllEnemiesKilled;
        public UnityEvent OnAllEnemiesSpawned;
        
        [Inject]
        public void Construct(EnemyPoolsContainer enemyPoolsContainer, Player player)
        {
            _player = player.transform;
            _enemyPoolsContainer = enemyPoolsContainer;
            _spawnedEnemies = new HashSet<Entity>();
            foreach (var enemySpawnConfig in SpawnerConfig.EnemySpawnerConfigs)
            {
                _weightSum += enemySpawnConfig.Weight;
            }
        }

        public void LaunchSpawner()
        {
            if(_isSpawning) return;
            _isSpawning = true;
            _spawnerLoopCancellation?.Dispose();
            _spawnerLoopCancellation = new CancellationTokenSource();
            SpawnLoop(_spawnerLoopCancellation.Token);
        }
        
        public void StopSpawner()
        {
            _spawnerLoopCancellation.Cancel();
            _isSpawning = false;
        }
        
        private async UniTask SpawnLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested && _entitiesSpawned < SpawnerConfig.EntityCount)
            {
                SpawnEnemy(ChooseRandomEnemy());
                await UniTask.WaitForSeconds(SpawnerConfig.SpawnCooldown, cancellationToken: cancellationToken);
            }

            _isSpawning = false;
            if(!SpawnerConfig.Endless && _entitiesSpawned >= SpawnerConfig.EntityCount)
                OnAllEnemiesKilled?.Invoke();
        }

        private void OnDeath(Entity entity)
        {
            entity.Health.OnDeath -= OnDeath;
            if(!_spawnedEnemies.Contains(entity)) return;
            _entitiesKilled++;
            _spawnedEnemies.Remove(entity);
            if(!SpawnerConfig.Endless && _entitiesKilled >= SpawnerConfig.EntityCount)
                OnAllEnemiesSpawned?.Invoke();
        }
        
        private EnemySpawnConfig ChooseRandomEnemy()
        {
            float rnd = Random.Range(0, _weightSum);
            float buf = 0;
            
            foreach (var enemySpawnConfig in SpawnerConfig.EnemySpawnerConfigs)
            {
                buf += enemySpawnConfig.Weight;
                
                if (buf >= rnd)
                {
                    return enemySpawnConfig;
                }
            }
            
            return SpawnerConfig.EnemySpawnerConfigs[0];
        }

        private void SpawnEnemy(EnemySpawnConfig enemySpawnConfig)
        {
            Entity enemy = _enemyPoolsContainer.Get(enemySpawnConfig.EnemyType);
            enemy.gameObject.SetActive(true);
            enemy.transform.position = ChooseRandomPositionOnCircle();
            _entitiesSpawned++;
            _spawnedEnemies.Add(enemy);
            enemy.Health.OnDeath += OnDeath;
        }

        private Vector2 ChooseRandomPositionOnCircle()
        {
            float rndAngle = Random.Range(0, 360);
            Vector2 offset = Quaternion.AngleAxis(rndAngle, Vector3.forward) * Vector2.up * SpawnerConfig.SpawnRadius;
            return (Vector2)_player.position + offset;
        }
    }
}
