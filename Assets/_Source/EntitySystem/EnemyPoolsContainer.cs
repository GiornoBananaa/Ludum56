using System;
using System.Collections.Generic;
using System.Linq;
using EnemySystem;
using EntitySystem.MovementSystem;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace EntitySystem
{
    public class EnemyPoolsContainer
    {
        private readonly Dictionary<EntityType, ObjectPool<Entity>> _enemyPools = new Dictionary<EntityType, ObjectPool<Entity>>();
        
        public void AddPool(EntityType entityType, Func<Entity> create)
        {
            _enemyPools.TryAdd(entityType, new ObjectPool<Entity>(create));
        }
        
        public Entity Get(EntityType entityType)
        {
            return _enemyPools[entityType].Get();
        }
        
        public Entity Return(Entity enemy)
        {
            return _enemyPools[enemy.Stats.EntityType].Get();
        }
    }
}