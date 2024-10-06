using EnemySystem;
using UnityEngine;

namespace EntitySystem
{
    [CreateAssetMenu(menuName = "Entity/EntityData", fileName = "EntityData")]
    public class EntityDataSO : ScriptableObject
    {
        [field: SerializeField] public Enemy Prefab { get; private set; }
        [field: SerializeField] public EntityStats EntityStats { get; private set; }
    }
}