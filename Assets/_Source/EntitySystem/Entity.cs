using System.Collections.Generic;
using AudioSystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EntitySystem
{
    public abstract class Entity : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public EntityAttack[] Attacks { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [field: SerializeField] public EntityAnimationHandler AnimationHandler { get; private set; }
        [field: SerializeField] public EntitySoundPlayer SoundPlayer { get; private set; }
        [field: SerializeField] public Collider2D Hitbox { get; private set; }
        
        public HashSet<EntityAttack> ActiveAttacks { get; private set; }
        public Health Health { get; private set; }
        public EntityStats Stats { get; private set; }
        public DamageDealer DamageDealer { get; private set; }
        public float MaxAttackRange { get; private set; }
        
        private IEntityMovement _movement;
        private IEntityCombat _combat;
        private bool _entityStarted;
        
        public void Construct(IEntityMovement entityMovement, IEntityCombat entityCombat, 
            DamageDealer damageDealer, EntityStats entityStats, AudioVolumeSetter audioVolumeSetter)
        {
            Stats = entityStats;
            Health = new Health(this, Stats.Health);
            SoundPlayer.SetSoundConfig(Stats.EntitySoundConfig);
            audioVolumeSetter.AddAudioPlayer(SoundPlayer);
            ActiveAttacks = new HashSet<EntityAttack>();
            DamageDealer = damageDealer;
            _movement = entityMovement;
            _combat = entityCombat;
            for (var i = 0; i < Attacks.Length; i++)
            {
                var attack = Attacks[i];
                attack.SetConfig(this, Stats.AttackStat[i]);
                MaxAttackRange = attack.Stats.AttackRange;
            }
            if(NavMeshAgent != null)
                NavMeshAgent.speed = Stats.Speed;
            
            _entityStarted = true;
            OnEntityStart();
        }

        private void OnEnable()
        {
            if (_entityStarted)
                Reset();
        }

        protected virtual void OnEntityStart() { }

        private void Update()
        {
            if(!_entityStarted) return;
            _movement.HandleMovement(this);
            _combat.HandleCombat(this);
        }
        
        public void TakeDamage(int damage) 
            => Health.TakeDamage(damage);
        
        public void TurnOffCollision() 
            => Hitbox.enabled = false;
        
        public void Reset()
        {
            enabled = true; 
            Health.Reset();
            AnimationHandler.Reset();
            Hitbox.enabled = true;
        }
    }
}