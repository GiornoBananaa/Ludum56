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
            entity.SoundPlayer.PlayDeath();
            entity.AnimationHandler.PlayDeath();
            entity.NavMeshAgent.ResetPath();
            entity.enabled = false;
            entity.TurnOffCollision();
            await UniTask.WaitForSeconds(entity.AnimationHandler.DeathAnimationTime);
            if(entity==null) return;
            entity.gameObject.SetActive(false);
        }
    }
}