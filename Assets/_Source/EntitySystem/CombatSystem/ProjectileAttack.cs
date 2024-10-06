using System;
using UnityEngine;

namespace EntitySystem.CombatSystem
{
    public class ProjectileAttack : EntityAttack, IParticleCollisionListener
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private ParticleCollisionDetector _particleCollision;
        private float _timeAfterAttack;
        private bool _isCooldown;
        public override bool CanAttack => !_isCooldown;

        private void OnEnable()
        {
            _particleCollision.Subscribe(this, Entity.Stats.AttackTargetLayer);
        }

        private void Update()
        {
            CheckCooldown();
        }

        private void CheckCooldown()
        {
            _timeAfterAttack += Time.deltaTime;
            if (_isCooldown && _timeAfterAttack >= Stats.AttackCooldown)
                _isCooldown = false;
        }
        
        public override void Attack(Entity target)
        {
            if(!CanAttack) return;
            _particleSystem.transform.LookAt(target.transform);
            _particleSystem.Play();
            _isCooldown = true;
        }

        private void DealDamage(Entity target)
        {
            Entity.DamageDealer.DealDamage(target, new DamageInfo(Stats, Entity));
        }
        
        public void OnParticleCollisionEnter(GameObject other)
        {
            if (other.TryGetComponent(out Entity entity))
            {
                DealDamage(entity);
            }
        }
    }

    public interface IParticleCollisionListener
    {
        void OnParticleCollisionEnter(GameObject other);
    }
}