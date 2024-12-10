using EntitySystem;
using UnityEngine;
using UnityEngine.UI;

namespace EnemySystem
{
    public class EnemyBossHealthBarView : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private Image _bar;

        private void Start()
        {
            _entity.Health.OnTakeDamage += ChangeHealth;
        }
        
        private void ChangeHealth(int health)
        {
            if (_entity == null) return;
            _bar.fillAmount = _entity.Health.HP > 0 ? (float)_entity.Health.HP / _entity.Health.MaxHP : 0;
        }
    }
}