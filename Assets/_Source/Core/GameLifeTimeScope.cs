using AudioSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Core.DataLoadingSystem;
using EnemySystem;
using EntitySystem;
using EntitySystem.CombatSystem;
using EntitySystem.MovementSystem;
using LevelSystem;

namespace Core
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private Player _player;
        [SerializeField] private ScreenFade _screenFade;
        [SerializeField] private LevelResultsView _levelResultsView;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private EntitySpawner[] _entitySpawner;
        [SerializeField] private BossCutscene _bossCutscene;
        
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
            
            #region UI
            builder.RegisterComponent(_levelResultsView);
            builder.RegisterComponent(_screenFade);
            #endregion
            
            #region Level
            builder.RegisterComponent(_levelManager);
            if(_bossCutscene != null)
                builder.RegisterComponent(_bossCutscene);
            #endregion
            
            #region Enemy

            foreach (var spawner in _entitySpawner)
            {
                builder.RegisterComponent(spawner).As<EntitySpawner>();
            }
            
            builder.Register<MeleeEnemyFactory>(Lifetime.Scoped);
            builder.Register<ProjectileEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, MeleeEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, SecondMeleeEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, ProjectileEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, SecondProjectileEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, BossEnemyFactory>(Lifetime.Scoped);
            builder.Register<IEntityMovement, EnemyMovement>(Lifetime.Singleton);
            builder.Register<IEntityCombat, EntityCombat>(Lifetime.Singleton);
            builder.Register<EnemyPoolsContainer>(Lifetime.Singleton);
            builder.Register<EnemyDeathHandler>(Lifetime.Singleton);
            #endregion
            
            #region Audio
            builder.Register<AudioVolumeSetter>(Lifetime.Singleton);
            #endregion
        }
        
        private void LoadResources(IResourceLoader resourceLoader, IRepository<ScriptableObject> dataRepository)
        {
            resourceLoader.LoadResource(PathData.ENEMY_DATA_PATH,
              typeof(EntityDataSO), dataRepository);
        }
    }
}