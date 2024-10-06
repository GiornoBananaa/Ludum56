using System;
using UnityEngine;

namespace EnemySystem
{
    public class EntitySoundHandler: MonoBehaviour
    {
        [Serializable]
        public class SoundConfig
        {
            public AudioClip Death;
            public AudioClip[] Attacks;
        }
        
        [SerializeField] private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource.playOnAwake = false;
        }

        private SoundConfig _soundConfig;

        public void SetSoundConfig(SoundConfig soundConfig)
        {
            _soundConfig = soundConfig;
        }
        
        public void PlayAttack(int attack)
        {
            PlaySound(_soundConfig.Attacks[attack]);
        } 
        
        public void PlayDeath()
        {
            PlaySound(_soundConfig.Death);
        }

        public void PlaySound(AudioClip audioClip)
        {
            if(audioClip == null)
            {
                Debug.LogWarning("Sound is null");
                return;
            }
            _audioSource.Stop();
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}