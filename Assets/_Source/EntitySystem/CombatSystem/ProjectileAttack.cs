using System;
using UnityEngine;
using Utils;

namespace EntitySystem.CombatSystem
{
    public class ProjectileAttack : EntityAttack, IParticleCollisionListener
    {
        [SerializeField] private Transform _attackEffect;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private ParticleCollisionDetector _particleCollision;
        private float _timeAfterAttack;
        private bool _isCooldown;
        public override bool CanAttack => !_isCooldown;

        private void Start()
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
        
        public override void Attack(Transform targetTransform, IDamageable target)
        {
            if(!CanAttack) return;
            AimAtTarget(targetTransform);
            _particleSystem.Play();
            _isCooldown = true;
            _timeAfterAttack = 0;
        }

        private void AimAtTarget(Transform targetTransform)
        {
            _attackEffect.transform.LookAt2D(targetTransform);
            _particleSystem.transform.LookAt(targetTransform, Vector3.forward);
        }
        
        private void DealDamage(IDamageable target)
        {
            Entity.DamageDealer.DealDamage(target, new DamageInfo(Stats, Entity));
        }
        
        public void OnParticleCollisionEnter(GameObject other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                DealDamage(damageable);
            }
        }
    }

    public interface IParticleCollisionListener
    {
        void OnParticleCollisionEnter(GameObject other);
    }
}