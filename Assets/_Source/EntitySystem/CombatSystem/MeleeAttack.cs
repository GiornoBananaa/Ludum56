using UnityEngine;
using Utils;

namespace EntitySystem.CombatSystem
{
    public class MeleeAttack : EntityAttack
    {
        [SerializeField] private Transform _attackEffect;
        private float _timeAfterAttack;
        private bool _isCooldown;
        public override bool CanAttack => !_isCooldown;
        
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
            _attackEffect.transform.LookAt2D(targetTransform);
            Entity.DamageDealer.DealDamage(target, new DamageInfo(Stats, Entity));
            _isCooldown = true;
            _timeAfterAttack = 0;
        }
    }
}