using System.Collections.Generic;
using AudioSystem;
using EnemySystem;
using EntitySystem;
using VContainer.Unity;

namespace Core
{
    public class Bootstrapper : IStartable
    {
        private readonly AudioVolumeSetter _audioVolumeSetter;

        public Bootstrapper(IEnumerable<EnemyFactory> factories, IEnumerable<EntitySpawner> spawners,
            AudioVolumeSetter audioVolumeSetter)
        {
            _audioVolumeSetter = audioVolumeSetter;
        }

        void IStartable.Start()
        {
            _audioVolumeSetter.LoadVolumeData();
        }
    }
}