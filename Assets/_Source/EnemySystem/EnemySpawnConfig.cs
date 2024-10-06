using System;
using EntitySystem;

namespace EnemySystem
{
    [Serializable]
    public class EnemySpawnConfig
    {
        public EntityType EnemyType;
        public float Weight;
    }
}