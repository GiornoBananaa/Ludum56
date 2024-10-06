using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using EntitySystem;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private SpawnerConfigSO SpawnerConfig;
        
        private EnemyPoolsContainer _enemyPoolsContainer;
        private CancellationTokenSource _spawnerLoopCancellation;
        private Transform _player;
        private float _weightSum;
        private int _entitiesSpawned;
        
        [Inject]
        public void Construct(EnemyPoolsContainer enemyPoolsContainer)/*, Player player*/
        {
            /*_player = player;*/
            Debug.Log(_enemyPoolsContainer == null);
            _enemyPoolsContainer = enemyPoolsContainer;
            foreach (var enemySpawnConfig in SpawnerConfig.EnemySpawnerConfigs)
            {
                _weightSum += enemySpawnConfig.Weight;
            }
        }

        public void LaunchSpawner()
        {
            _spawnerLoopCancellation?.Dispose();
            _spawnerLoopCancellation = new CancellationTokenSource();
            SpawnLoop(_spawnerLoopCancellation.Token);
        }
        
        public void StopSpawner()
        {
            _spawnerLoopCancellation.Cancel();
        }
        
        private async UniTask SpawnLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested && _entitiesSpawned < SpawnerConfig.EntityCount)
            {
                SpawnEnemy(ChooseRandomEnemy());
                await UniTask.WaitForSeconds(SpawnerConfig.SpawnCooldown, cancellationToken: cancellationToken);
            }
        }
        
        private EnemySpawnConfig ChooseRandomEnemy()
        {
            float rnd = Random.Range(0, _weightSum + 1f);
            float buf = 0;
            
            foreach (var enemySpawnConfig in SpawnerConfig.EnemySpawnerConfigs)
            {
                buf += enemySpawnConfig.Weight;
                if (rnd > buf)
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
        }

        private Vector2 ChooseRandomPositionOnCircle()
        {
            float rndAngle = Random.Range(0, 360);
            Vector2 offset = Quaternion.AngleAxis(rndAngle, Vector3.forward) * Vector2.up * SpawnerConfig.SpawnRadius;
            return /*_player.position*/ Vector2.zero + offset;
        }
    }
}
