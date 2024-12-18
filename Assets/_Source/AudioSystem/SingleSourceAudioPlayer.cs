﻿using System;
using UnityEngine;

namespace AudioSystem
{
    public abstract class SingleSourceAudioPlayer : MonoBehaviour, IAudioPlayer
    {
        [Serializable]
        public class SoundConfig
        {
            public AudioClip Clip;
            public float MaxVolume = 1;
        }
        
        [SerializeField] protected AudioSource AudioSource;
        protected float Volume = 1;
        protected SoundConfig CurrentSound;
        
        public abstract AudioType AudioType { get; }

        public void SetVolume(float volume)
        {
            Volume = volume;
            if (CurrentSound != null)
                SetSourceVolume();
        }
        
        protected void PlaySound(SoundConfig soundConfig)
        {
            if(soundConfig.Clip == null)
            {
                Debug.LogWarning("Sound clip is null");
                return;
            }
            CurrentSound = soundConfig;
            AudioSource.Stop();
            SetSourceVolume();
            AudioSource.clip = soundConfig.Clip;
            AudioSource.Play();
        }

        private void SetSourceVolume()
        {
            AudioSource.volume = Volume * CurrentSound.MaxVolume;
        }
    }
}