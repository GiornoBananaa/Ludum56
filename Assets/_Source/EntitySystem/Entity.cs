using System;
using System.Collections.Generic;
using Core.DataLoadingSystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EntitySystem
{
    public abstract class Entity : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public EntityAttack[] EntityAttacks { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        public HashSet<EntityAttack> ActiveAttacks { get; private set; }
        public Health Health { get; private set; }
        public EntityStats Stats { get; private set; }
        public DamageDealer DamageDealer { get; private set; }
        public float MaxAttackRange { get; private set; }
        
        private IEntityMovement _entityMovement;
        private IEntityCombat _entityCombat;
        private bool _entityStarted;
        
        public void Construct(IEntityMovement entityMovement, IEntityCombat entityCombat, 
            DamageDealer damageDealer,EntityStats entityStats)
        {
            Stats = entityStats;
            Health = new Health(this, Stats.Health);
            ActiveAttacks = new HashSet<EntityAttack>();
            DamageDealer = damageDealer;
            _entityMovement = entityMovement;
            _entityCombat = entityCombat;
            for (var i = 0; i < EntityAttacks.Length; i++)
            {
                var attack = EntityAttacks[i];
                attack.SetConfig(this, Stats.AttackStat[i]);
                MaxAttackRange = attack.Stats.AttackRange;
            }
            if(NavMeshAgent != null)
                NavMeshAgent.speed = Stats.Speed;
            
            _entityStarted = true;
            OnEntityStart();
        }
        
        protected virtual void OnEntityStart() { }

        private void Update()
        {
            if(!_entityStarted) return;
            _entityMovement.HandleMovement(this);
            _entityCombat.HandleCombat(this);
        }
        
        public void TakeDamage(int damage) 
            => Health.TakeDamage(damage);

        public void Reset()
        {
            Health.Reset();
        }
    }
}