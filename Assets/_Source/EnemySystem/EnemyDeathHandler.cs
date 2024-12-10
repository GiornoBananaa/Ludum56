using Cysharp.Threading.Tasks;
using EntitySystem;
using UnityEngine;

namespace EnemySystem
{
    public class EnemyDeathHandler
    {
        private EnemyPoolsContainer _enemyPoolsContainer;
        
        public EnemyDeathHandler(EnemyPoolsContainer enemyPoolsContainer)
        {
            _enemyPoolsContainer = enemyPoolsContainer;
        }
        
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
            if(entity == null) return;
            entity.gameObject.SetActive(false);
            _enemyPoolsContainer.Return(entity);
        }
    }
}