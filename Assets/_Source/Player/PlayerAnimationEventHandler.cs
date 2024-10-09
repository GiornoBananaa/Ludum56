using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private PlayerAudioPlayer _playerAudioPlayer;
    
    public void PlayAttackSound()
    {
        _playerAudioPlayer.PlayAttack();
    }
    
    public void PlayStepSound()
    {
        _playerAudioPlayer.PlayStep();
    }
    
    public void EnableDamageDealing()
    {
        _playerControl.EnableDamageDealer();
    }
    
    public void DisableDamageDealing()
    {
        _playerControl.DisableDamageDealer();
    }
}