using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace AudioSystem
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;

        private AudioVolumeSetter _audioVolumeSetter;
        
        [Inject]
        public void Construct(AudioVolumeSetter audioVolumeSetter)
        {
            _audioVolumeSetter = audioVolumeSetter;
        }
        
        private void Awake()
        {
            if(_musicSlider!=null) _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
            if(_soundSlider!=null) _soundSlider.onValueChanged.AddListener(ChangeSoundEffectVolume);

            _musicSlider.value = _audioVolumeSetter.GetVolume(AudioType.Music);
            _soundSlider.value = _audioVolumeSetter.GetVolume(AudioType.SoundEffect);
        }

        private void ChangeMusicVolume(float volume)
        {
            _audioVolumeSetter.SetVolume(AudioType.Music, volume);
        }
        
        private void ChangeSoundEffectVolume(float volume)
        {
            _audioVolumeSetter.SetVolume(AudioType.SoundEffect, volume);
        }
    }
}