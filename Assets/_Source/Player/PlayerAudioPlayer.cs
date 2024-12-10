using AudioSystem;
using UnityEngine;
using AudioType = AudioSystem.AudioType;

public class PlayerAudioPlayer : SingleSourceAudioPlayer
{
    public override AudioType AudioType => AudioType.SoundEffect;

    [SerializeField] private SoundConfig _attackSound;
    [SerializeField] private SoundConfig _deathSound;
    [SerializeField] private SoundConfig _stepSound;

    private bool _deathIsPlaying;
    public void PlayAttack()
    {
        if(_deathIsPlaying) return;
        PlaySound(_attackSound);
    }
    
    public void PlayDeath()
    {
        _deathIsPlaying = true;
        PlaySound(_deathSound);
    }
    
    public void PlayStep()
    {
        if(_deathIsPlaying) return;
        PlaySound(_stepSound);
    }
}