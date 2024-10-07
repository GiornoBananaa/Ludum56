using UnityEngine;
using UnityEngine.Events;

namespace EnemySystem
{
    public abstract class EntitySpawner : MonoBehaviour
    {
        public abstract bool AllEntitiesKilled { get; protected set; }
        public abstract int EntitiesKilledCount { get; }
        public abstract UnityEvent OnAllEntitiesKilled { get; protected set; }
        public abstract UnityEvent OnAllEnemiesSpawned { get; protected set; }
        public abstract void LaunchSpawner();
    }
}