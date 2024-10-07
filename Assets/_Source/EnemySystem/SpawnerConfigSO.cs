using UnityEngine;

namespace EnemySystem
{
    [CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Entity/SpawnerConfig")]
    public class SpawnerConfigSO : ScriptableObject
    {
        [field: SerializeField] public float SpawnCooldown { get; private set; }
        [field: SerializeField] public float SpawnRadius { get; private set; }
        [field: SerializeField] public bool Endless { get; private set; }
        [field: SerializeField] public int EntityCount { get; private set; }
        [field: SerializeField] public EnemySpawnConfig[] EnemySpawnerConfigs { get; private set; }
    }
}