using Cysharp.Threading.Tasks;
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

        protected virtual async void OnDeath(Entity entity)
        {
            entity.SoundHandler.PlayDeath();
            entity.AnimationHandler.PlayDeath();
            entity.NavMeshAgent.ResetPath();
            entity.enabled = false;
            await UniTask.WaitForSeconds(1);
            entity.gameObject.SetActive(false);
        }
    }
}