namespace EntitySystem.CombatSystem
{
    public struct DamageInfo
    {
        public AttackStat AttackStat;
        public Entity DamageDealer;

        public DamageInfo(AttackStat attackStat, Entity damageDealer)
        {
            AttackStat = attackStat;
            DamageDealer = damageDealer;
        }
    }
}