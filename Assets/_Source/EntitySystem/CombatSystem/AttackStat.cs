using System;

namespace EntitySystem.CombatSystem
{
    [Serializable]
    public class AttackStat
    {
        public int Damage = 1;
        public float AttackRange = 1;
        public float AttackCooldown = 1;
        public int Weight { get; private set; } = 1;
        public bool LongAttack;
    }
}