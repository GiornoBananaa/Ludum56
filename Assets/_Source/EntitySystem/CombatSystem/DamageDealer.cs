namespace EntitySystem.CombatSystem
{
    public class DamageDealer
    {
        public virtual void DealDamage(IDamageable damageable, DamageInfo damageInfo)
        {
            damageable.TakeDamage(damageInfo.AttackStat.Damage);
        }
    }
}