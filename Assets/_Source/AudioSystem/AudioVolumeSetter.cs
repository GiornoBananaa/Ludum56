using System;
using System.Collections.Generic;
using UnityEngine;
namespace AudioSystem
{
    public class AudioVolumeSetter
    {
        private Dictionary<AudioType, List<IAudioPlayer>> _audioPlayers;
        
        public AudioVolumeSetter(IEnumerable<IAudioPlayer> audioPlayers)
        {
            _audioPlayers = new Dictionary<AudioType, List<IAudioPlayer>>();
            _volume = new Dictionary<AudioType, float>();
            
            foreach (AudioType audioType in Enum.GetValues(typeof(AudioType)))
            {
                _audioPlayers.Add(audioType, new List<IAudioPlayer>());
                foreach (var audioPlayer in audioPlayers)
                {
                    if(audioPlayer.AudioType == audioType)
                        _audioPlayers[audioType].Add(audioPlayer);
                }
            }
            LoadVolumeData();
        }
    
        private Dictionary<AudioType, float> _volume;
        
        public void AddAudioPlayer(IAudioPlayer audioPlayer)
        {
            _audioPlayers[audioPlayer.AudioType].Add(audioPlayer);
            audioPlayer.SetVolume(_volume[audioPlayer.AudioType]);
        }
        
        public void SetVolume(AudioType audioType, float volume)
        {
            _volume[audioType] = volume;
            foreach (var audioPlayer in _audioPlayers[audioType])
            {
                audioPlayer.SetVolume(volume);
            }
        }
        
        public void LoadVolumeData()
        {
            foreach (AudioType audioType in Enum.GetValues(typeof(AudioType)))
            {
                SetVolume(audioType, PlayerPrefs.GetFloat("Volume" + (int)audioType, 1f));
            }
        }
        
        public void SaveVolumeData()
        {
            foreach (var keyValuePair in _volume)
            {
                PlayerPrefs.GetFloat("Volume" + (int)keyValuePair.Key, keyValuePair.Value);
            }
            PlayerPrefs.Save();
        }
    }
}


