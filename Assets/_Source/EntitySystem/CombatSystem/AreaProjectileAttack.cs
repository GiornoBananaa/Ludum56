﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EntitySystem.CombatSystem
{
    public class AreaProjectileAttack : EntityAttack, IParticleCollisionListener
    {
        [Serializable]
        public class AreaProjectileAttackConfig
        {
            public int ShotsCount = 10;
            public float RotationSpeed = 1;
            public float ShotsGapTime = 0.3f;
            public ParticleSystem[] ParticleSystem;
        }
        
        [SerializeField] private AreaProjectileAttackConfig[] _configs;
        [SerializeField] private ParticleCollisionDetector[] _particleCollision;
        [SerializeField] private EntityAnimationHandler _entityAnimationHandler;
        
        private CancellationTokenSource _attackCancellation;
        private float _timeAfterAttack;
        private float _directionOffset;
        private int _configIndex;
        private bool _isCooldown;
        private bool _isAttacking;
        
        public override bool IsAttacking => _isAttacking;
        public override bool CanAttack => !_isCooldown && !_isAttacking;

        private void Start()
        {
            foreach (var particle in _particleCollision)
            {
                particle.Subscribe(this, Entity.Stats.AttackTargetLayer);
            }
        }

        private void Update()
        {
            CheckCooldown();
            AimAtTarget();
        }

        private void OnDestroy()
        {
            _attackCancellation?.Cancel();
        }

        private void CheckCooldown()
        {
            if(_isAttacking) return;
            _timeAfterAttack += Time.deltaTime;
            if (_isCooldown && _timeAfterAttack >= Stats.AttackCooldown)
                _isCooldown = false;
        }
        
        public override void Attack(Transform targetTransform, IDamageable target)
        {
            if(!CanAttack) return;
            _configIndex = Random.Range(0,_configs.Length);
            _isCooldown = true;
            _isAttacking = true;
            _timeAfterAttack = 0;
            _attackCancellation?.Dispose();
            _attackCancellation = new CancellationTokenSource();
            AreaAttack(_attackCancellation.Token);
        }

        private async UniTaskVoid AreaAttack(CancellationToken cancellationToken)
        {
            _entityAnimationHandler.SetAttack(true);
            for (int i = 0; i < _configs[_configIndex].ShotsCount; i++)
            {
                if(Entity.Health.HP <= 0) break;
                foreach (var particle in _configs[_configIndex].ParticleSystem)
                {
                    particle.Play();
                }
                await UniTask.WaitForSeconds(_configs[_configIndex].ShotsGapTime, cancellationToken: cancellationToken);
            }
            _entityAnimationHandler.SetAttack(false);
            _timeAfterAttack = 0;
            _isAttacking = false;
        }
        
        private void AimAtTarget()
        {
            _directionOffset += Time.deltaTime * _configs[_configIndex].RotationSpeed;
            float step = 360f / _configs[_configIndex].ParticleSystem.Length;
            float angle = _directionOffset;
            foreach (var particle in _configs[_configIndex].ParticleSystem)
            {
                Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
                particle.transform.rotation = Quaternion.LookRotation(direction);
                angle += step;
            }
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
}