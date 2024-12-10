using UnityEngine;

namespace AudioSystem
{
    [CreateAssetMenu(menuName = "Audio/AudioConfig", fileName = "AudioConfig")]
    public class AudioConfigSO : ScriptableObject
    {
        [field: SerializeField] public MusicPlayer.MusicConfig[] Music { get; private set; }
    }
}