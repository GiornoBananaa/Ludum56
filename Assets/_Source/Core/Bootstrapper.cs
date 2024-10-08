using System.Collections.Generic;
using EnemySystem;
using EntitySystem;
using VContainer.Unity;

namespace Core
{
    public class Bootstrapper : IStartable
    {
        public Bootstrapper(IEnumerable<EnemyFactory> factories, IEnumerable<EntitySpawner> spawner)
        {
            
        }

        void IStartable.Start()
        {
            
        }
    }
}