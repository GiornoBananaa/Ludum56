using System;
using UnityEngine;
using Utils;

namespace EntitySystem.CombatSystem
{
    public class ProjectileAttack : EntityAttack, IParticleCollisionListener
    {
        [SerializeField] private bool _predictPosition;
        [SerializeField] private Transform _attackEffect;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private ParticleCollisionDetector _particleCollision;
        private Transform _target = null;
        private Vector2 _lastPlayerPosition;
        private Vector2 _projectileDirection;
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
            PredictTargetPosition();
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
            if (_target != targetTransform)
            {
                _target = targetTransform;
                _lastPlayerPosition = targetTransform.position;
            }

            Vector3 lookAt = _predictPosition ? transform.position + (Vector3)_projectileDirection : targetTransform.position;
            _attackEffect.transform.LookAt2D(lookAt);
            _particleSystem.transform.LookAt(lookAt, Vector3.forward);
        }

        private void PredictTargetPosition()
        {
            if (_target == null)
                return;
            Vector2 targetVelocity = (_lastPlayerPosition - (Vector2)_target.position)/Time.deltaTime;
            Vector2 start = _particleSystem.transform.position;
            Vector2 target = _target.position;
            float speed = _particleSystem.main.startSpeed.constant;
            Vector2 estimatedVelocity = (target - start).normalized * speed;
            float distance = Vector3.Distance(start, target);
            Vector2 relativeVelocity = estimatedVelocity - targetVelocity;
            float time = distance / relativeVelocity.magnitude;
            Vector2 futurePosition = target - targetVelocity * time;
            _projectileDirection = Vector2.Angle(target - start, futurePosition) < 100  ?  (futurePosition - start).normalized : (target - start).normalized;
            _lastPlayerPosition =  _target.position;
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