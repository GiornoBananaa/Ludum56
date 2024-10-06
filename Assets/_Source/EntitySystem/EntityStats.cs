using System;
using EnemySystem;
using EntitySystem.CombatSystem;
using UnityEngine;

namespace EntitySystem
{
    [Serializable]
    public class EntityStats
    {
        [field: SerializeField] public EntityType EntityType { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float StopRange { get; private set; }
        [field: SerializeField] public LayerMask AttackTargetLayer { get; private set; }
        [field: SerializeField] public AttackStat[] AttackStat { get; private set; }
        [field: SerializeField] public EntitySoundHandler.SoundConfig SoundConfig { get; private set; }
    }
}