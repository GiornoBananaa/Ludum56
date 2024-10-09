using UnityEngine;

namespace EntitySystem
{
    public class EntityAnimationHandler: MonoBehaviour
    {
        private static readonly string _attackProperty = "Attack";
        private static readonly int _deathProperty = Animator.StringToHash("Death");
        private static readonly int _longAttackProperty = Animator.StringToHash("LongAttack");
        private static readonly int _isAttackProperty = Animator.StringToHash("IsAttack");

        [field: SerializeField] public float DeathAnimationTime { get; private set; }
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;
        
        public void PlayAttack(int attack)
        {
            _animator.SetTrigger(_attackProperty + attack);
        } 
        
        public void SetLongAttack(bool value)
        {
            _animator.SetBool(_longAttackProperty, value);
        }
        
        public void SetAttack(bool value)
        {
            _animator.SetBool(_isAttackProperty, value);
        }
        
        public void Rotate(bool right)
        {
            _renderer.flipX = right;
        } 
        
        public void PlayDeath()
        {
            _animator.SetTrigger(_deathProperty);
        }

        public void Reset()
        {
            _animator.Rebind();
            _animator.Update(0f);
            
        }
    }
}