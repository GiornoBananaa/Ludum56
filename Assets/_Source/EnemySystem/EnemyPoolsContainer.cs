using System;
using System.Collections.Generic;
using EntitySystem;
using UnityEngine.Pool;

namespace EnemySystem
{
    public class EnemyPoolsContainer
    {
        private readonly Dictionary<EntityType, ObjectPool<Entity>> _enemyPools = new();
        
        public void AddPool(EntityType entityType, Func<Entity> create)
        {
            _enemyPools.TryAdd(entityType, new ObjectPool<Entity>(create));
        }
        
        public Entity Get(EntityType entityType)
        {
            var enemy = _enemyPools[entityType].Get();
            return enemy;
        }
        
        public void Return(Entity enemy)
        {
            _enemyPools[enemy.Stats.EntityType].Release(enemy);
        }
    }
}