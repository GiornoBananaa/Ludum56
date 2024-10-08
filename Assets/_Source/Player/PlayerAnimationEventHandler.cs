using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;
    
    public void EnableDamageDealing()
    {
        _playerControl.EnableDamageDealer();
    }
    
    public void DisableDamageDealing()
    {
        _playerControl.DisableDamageDealer();
    }
}