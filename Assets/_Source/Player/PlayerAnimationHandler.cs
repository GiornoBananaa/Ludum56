using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private const string ATTACK_PROPERTY = "Attack";
    private const string SKILL_PROPERTY = "Skill";
    private const string MOVE_PROPERTY = "Move";
    private const string DEATH_PROPERTY = "Death";
    private const string NEXTLEVEL_PROPERTY = "NextLevel";
    
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    
    public void PlayAttack()
    {
        _animator.SetTrigger(ATTACK_PROPERTY);
    }
    
    public void PlaySkill()
    {
        _animator.SetBool(SKILL_PROPERTY, true);
    }
    
    public void CancelSkill()
    {
        _animator.SetBool(SKILL_PROPERTY, false);
    }
    
    public void SetMove(bool value)
    {
        _animator.SetBool(MOVE_PROPERTY, value);
    }
    
    public void TurnSprite(bool right)
    {
        foreach (var renderer in _spriteRenderers)
        {
            renderer.flipX = right;
        }
    }

    public void PlayDeath()
    {
        _animator.SetTrigger(DEATH_PROPERTY);
    }

    public void PlayLevelSwitch()
    {
        _animator.SetTrigger(NEXTLEVEL_PROPERTY);
    }
}