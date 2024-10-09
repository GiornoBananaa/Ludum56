using System;
using AudioSystem;
using AudioType = AudioSystem.AudioType;

namespace EntitySystem
{
    public class EntitySoundPlayer: SingleSourceAudioPlayer
    {
        [Serializable]
        public class EntitySoundConfig
        {
            public SoundConfig Death;
            public SoundConfig[] Attacks;
        }
        
        private EntitySoundConfig _entitySoundConfig;
        
        public override AudioType AudioType => AudioType.SoundEffect;
        
        public void SetSoundConfig(EntitySoundConfig entitySoundConfig)
        {
            _entitySoundConfig = entitySoundConfig;
        }
        
        public void PlayAttack(int attack)
        {
            if(_entitySoundConfig.Attacks.Length <= attack) return;
            PlaySound(_entitySoundConfig.Attacks[attack]);
        } 
        
        public void PlayDeath()
        {
            PlaySound(_entitySoundConfig.Death);
        }
    }
}