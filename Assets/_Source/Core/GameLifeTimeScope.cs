using UnityEngine;
using VContainer;
using VContainer.Unity;
using Core.DataLoadingSystem;

namespace Core
{
    public class GameLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrapper>();

            #region DataLoad
            IResourceLoader resourceLoader = new ResourceLoader();
            IRepository<ScriptableObject> dataRepository = new DataRepository<ScriptableObject>();

            LoadResources(resourceLoader, dataRepository);

            builder.RegisterInstance<IRepository<ScriptableObject>>(dataRepository);
            #endregion
            
            /*
            #region Input
            builder.RegisterComponent<InputListener>(_inputListener);
            #endregion
            */
            /*
            #region Enemy
            builder.Register<IFactory<Enemy>, EnemyFactory>(Lifetime.Singleton);
            
            #endregion
            */
        }
        
        private void LoadResources(IResourceLoader resourceLoader, IRepository<ScriptableObject> dataRepository)
        {
            /*
            resourceLoader.LoadResource(PathData.LEVEL_GENERATION_DATA_PATH,
              typeof(LevelGenerationDataSO), dataRepository);
              */
        }
    }
}
