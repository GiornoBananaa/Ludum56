using EntitySystem;
using EntitySystem.MovementSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyMovement : IEntityMovement
    {
        private Transform _player;
        
        public EnemyMovement(Player player)
        {
            _player = player.transform;
        }
        
        public void HandleMovement(Entity entity)
        {
            Vector2 offset = (_player.position - entity.transform.position).normalized * entity.Stats.StopRange;
            entity.NavMeshAgent.SetDestination(_player.position + (Vector3)offset);
        }
    }
}