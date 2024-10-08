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
            entity.AnimationHandler.Rotate(_player.position.x > entity.transform.position.x);
            entity.transform.rotation = Quaternion.identity;
            if (!(Vector2.Distance(entity.transform.position, _player.position) > entity.Stats.StopRange)) return;
            Vector2 offset = (entity.transform.position - _player.position).normalized * entity.Stats.StopRange;
            Vector2 destination = _player.position + (Vector3)offset;
            entity.NavMeshAgent.SetDestination(destination);
        }
    }
}