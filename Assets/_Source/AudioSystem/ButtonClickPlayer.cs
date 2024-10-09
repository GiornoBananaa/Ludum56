using UnityEngine;
using UnityEngine.UI;

namespace AudioSystem
{
    public class ButtonClickPlayer : SingleSourceAudioPlayer
    {
        [SerializeField] private Button _button;
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private AudioSource _audioSource;
        private float _defaultVolume;
        
        public override AudioType AudioType => AudioType.SoundEffect;
            
        private void Awake()
        {
            _defaultVolume = _audioSource.volume;
            _button.onClick.AddListener(PlayClick);
            _audioSource.clip = _clickSound;
        }
            
        public void SetVolume(float volume)
        {
            _audioSource.volume = _defaultVolume * volume;
        }

        private void PlayClick()
        {
            _audioSource.Play();
        }
    }
}