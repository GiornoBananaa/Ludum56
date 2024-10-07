using UnityEngine;

namespace EnemySystem
{
    public class EntityAnimationHandler: MonoBehaviour
    {
        private static readonly string _attackProperty = "Attack";
        private static readonly int _deathProperty = Animator.StringToHash("Death");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;
        
        public void PlayAttack(int attack)
        {
            _animator.SetTrigger(_attackProperty + attack);
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