using System;

namespace EntitySystem.CombatSystem
{
    [Serializable]
    public class AttackStat
    {
        public int Damage = 1;
        public int AttackRange = 1;
        public int AttackCooldown = 1;
        public int Weight { get; private set; } = 1;
        public int MaxTargetsCount { get; private set; } = 999;
    }
}