using UnityEngine;
using VContainer;

namespace EntitySystem
{
    public class EnemyDeathHandler
    {
        public void Subscribe(Entity entity)
        {
            entity.Health.OnDeath += OnDeath;
        }
        
        public void UnSubscribe(Entity entity)
        {
            entity.Health.OnDeath -= OnDeath;
        }

        protected virtual void OnDeath(Entity entity)
        {
            entity.NavMeshAgent.ResetPath();
            entity.gameObject.SetActive(false);
            entity.SoundHandler.PlayDeath();
        }
    }
}