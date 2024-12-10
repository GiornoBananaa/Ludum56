using UnityEngine;
using UnityEngine.UI;

namespace AudioSystem
{
    public class ButtonClickPlayer : SingleSourceAudioPlayer
    {
        [SerializeField] private Button[] _buttons;
        [SerializeField] private SoundConfig _clickSound;
        
        public override AudioType AudioType => AudioType.SoundEffect;
            
        private void Awake()
        {
            foreach (var button in _buttons)
            {
                button.onClick.AddListener(PlayClick);
            }
        }

        private void PlayClick()
        {
            PlaySound(_clickSound);
        }
    }
}