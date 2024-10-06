using UnityEngine;
using VContainer;
using VContainer.Unity;
using Core.DataLoadingSystem;
using EnemySystem;
using EntitySystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using InputSystem;

namespace Core
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private Player _player;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrapper>();

            #region DataLoad
            IResourceLoader resourceLoader = new ResourceLoader();
            IRepository<ScriptableObject> dataRepository = new DataRepository<ScriptableObject>();
            LoadResources(resourceLoader, dataRepository);
            builder.RegisterInstance(dataRepository);
            #endregion
            
            #region Player
            builder.RegisterComponent(_player);
            #endregion
            
            #region Enemy
            builder.Register<MeleeEnemyFactory>(Lifetime.Scoped);
            builder.Register<ProjectileEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, MeleeEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, ProjectileEnemyFactory>(Lifetime.Scoped);
            builder.Register<IEntityMovement, EnemyMovement>(Lifetime.Singleton);
            builder.Register<IEntityCombat, EntityCombat>(Lifetime.Singleton);
            builder.Register<EnemyPoolsContainer>(Lifetime.Singleton);
            builder.Register<EnemyDeathHandler>(Lifetime.Singleton);
            #endregion
            
        }
        
        private void LoadResources(IResourceLoader resourceLoader, IRepository<ScriptableObject> dataRepository)
        {
            
            resourceLoader.LoadResource(PathData.ENEMY_DATA_PATH,
              typeof(EntityDataSO), dataRepository);
        }
    }
}