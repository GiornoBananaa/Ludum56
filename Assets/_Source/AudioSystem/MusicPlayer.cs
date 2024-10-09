using System;
using UnityEngine;

namespace AudioSystem
{
    public class MusicPlayer : SingleSourceAudioPlayer
    {
        public override AudioType AudioType => AudioType.Music;

        [SerializeField] private SoundConfig _soundConfig;
        
        private void Start()
        {
            AudioSource.loop = true;
            PlaySound(_soundConfig);
        }
    }
}