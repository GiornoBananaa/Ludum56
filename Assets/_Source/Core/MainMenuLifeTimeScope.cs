using AudioSystem;
using Core.DataLoadingSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class MainMenuLifeTimeScope : LifetimeScope
    {
        [SerializeField] private MusicPlayer _musicPlayer;
        [SerializeField] private ButtonClickPlayer[] _buttonClickPlayers;
        [SerializeField] private SettingsView _settings;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrapper>();
            
            #region DataLoad
            IResourceLoader resourceLoader = new ResourceLoader();
            IRepository<ScriptableObject> dataRepository = new DataRepository<ScriptableObject>();
            LoadResources(resourceLoader, dataRepository);
            builder.RegisterInstance(dataRepository);
            #endregion

            #region UI
            builder.RegisterComponent(_settings);
            #endregion
            
            #region Audio
            builder.Register<AudioVolumeSetter>(Lifetime.Singleton);
            builder.RegisterComponent(_musicPlayer).As<IAudioPlayer>();
            foreach (var audioPlayer in _buttonClickPlayers)
            {
                builder.RegisterInstance(audioPlayer).As<IAudioPlayer>();
            }
            #endregion
        }
        
        private void LoadResources(IResourceLoader resourceLoader, IRepository<ScriptableObject> dataRepository)
        {
            resourceLoader.LoadResource(PathData.AUDIO_DATA_PATH,
                typeof(AudioConfigSO), dataRepository);
        }
    }
}