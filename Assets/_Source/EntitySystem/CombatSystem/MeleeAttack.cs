using UnityEngine;

namespace EntitySystem.CombatSystem
{
    public class MeleeAttack : EntityAttack
    {
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
        
        public override void Attack(Entity target)
        {
            if(!CanAttack) return;
            Entity.DamageDealer.DealDamage(target, new DamageInfo(Stats, Entity));
            _isCooldown = true;
        }
    }
}