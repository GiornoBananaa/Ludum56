using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataLoadingSystem;
using UnityEngine;
using VContainer;

namespace AudioSystem
{
    public class MusicPlayer : SingleSourceAudioPlayer
    {
        [Serializable]
        public class MusicConfig
        {
            public MusicType MusicType;
            public SoundConfig Config;
        }
        
        public override AudioType AudioType => AudioType.Music;

        [SerializeField] private MusicType _startMusic;
        private Dictionary<MusicType, MusicConfig> _musicClips;
        
        [Inject]
        public void Construct(IRepository<ScriptableObject> dataRepository)
        {
            AudioConfigSO repository = dataRepository.GetItem<AudioConfigSO>()[0];
            _musicClips = repository.Music.ToDictionary(c => c.MusicType);
        }
        
        private void Start()
        {
            AudioSource.loop = true;
            PlaySound(_musicClips[_startMusic].Config);
        }

        public void Play(MusicType musicType)
        {
            PlaySound(_musicClips[musicType].Config);
        }
    }
}