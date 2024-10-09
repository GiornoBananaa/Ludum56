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
using Menu;

namespace Core
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private Player _player;
        [SerializeField] private ScreenFade _screenFade;
        [SerializeField] private LevelResultsView _levelResultsView;
        [SerializeField] private LevelSwitcher _levelSwitcher;
        [SerializeField] private EntitySpawner[] _entitySpawner;
        [SerializeField] private BossCutscene _bossCutscene;
        [SerializeField] private MusicPlayer _musicPlayer;
        [SerializeField] private PlayerAudioPlayer _playerAudioPlayer;
        [SerializeField] private SettingsView _settings;
        [SerializeField] private ButtonClickPlayer[] _buttonClickPlayers;
        
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
            builder.RegisterComponent(_settings);
            #endregion
            
            #region Level
            builder.Register<LevelCounter>(Lifetime.Singleton);
            builder.RegisterComponent(_levelSwitcher);
            if(_bossCutscene != null)
                builder.RegisterComponent(_bossCutscene);
            #endregion
            
            #region Enemy

            foreach (var spawner in _entitySpawner)
            {
                builder.RegisterComponent(spawner).As<EntitySpawner>();
            }
            
            builder.Register<BrownMeleeEnemyFactory>(Lifetime.Scoped);
            builder.Register<RedProjectileEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, BrownMeleeEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, PurpleMeleeEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, RedProjectileEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, BlueProjectileEnemyFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory, BossEnemyFactory>(Lifetime.Scoped);
            builder.Register<IEntityMovement, EnemyMovement>(Lifetime.Singleton);
            builder.Register<IEntityCombat, EntityCombat>(Lifetime.Singleton);
            builder.Register<EnemyPoolsContainer>(Lifetime.Singleton);
            builder.Register<EnemyDeathHandler>(Lifetime.Singleton);
            #endregion
            
            #region Audio
            builder.Register<AudioVolumeSetter>(Lifetime.Singleton);
            builder.RegisterComponent(_musicPlayer).As<IAudioPlayer>();
            builder.RegisterComponent(_playerAudioPlayer).As<IAudioPlayer>();
            foreach (var audioPlayer in _buttonClickPlayers)
            {
                builder.RegisterInstance(audioPlayer).As<IAudioPlayer>();
            }
            #endregion
        }
        
        private void LoadResources(IResourceLoader resourceLoader, IRepository<ScriptableObject> dataRepository)
        {
            resourceLoader.LoadResource(PathData.ENEMY_DATA_PATH,
              typeof(EntityDataSO), dataRepository);
            resourceLoader.LoadResource(PathData.AUDIO_DATA_PATH,
                typeof(AudioConfigSO), dataRepository);
        }
    }
}