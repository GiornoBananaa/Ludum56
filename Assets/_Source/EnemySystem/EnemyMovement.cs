using EntitySystem;
using EntitySystem.MovementSystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyMovement : IEntityMovement
    {
        private Transform _player;
        
        /*
        public EnemyMovement(Player player)
        {
            _player = player.transform;
        }
        */
        
        public void HandleMovement(Entity entity)
        {
            entity.NavMeshAgent.SetDestination(/*_player.position*/Vector2.zero);
        }
    }
}