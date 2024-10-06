using UnityEngine;

namespace EntitySystem
{
    public class SpriteRenderOrderOnCamera : MonoBehaviour
    {
        private const int MIN_LAYER = 100;
        private const int MAX_LAYER = 200;
    
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private SpriteRenderer[] _spritesOnTop;
    
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            SetSortingOrder();
        }

        private void SetSortingOrder()
        {
            Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
            _spriteRenderer.sortingOrder = (int)Mathf.Lerp(MIN_LAYER,MAX_LAYER, (_camera.pixelHeight - screenPosition.y) / _camera.pixelHeight);
            for (int i = 0; i < _spritesOnTop.Length; i++)
            {
                _spritesOnTop[i].sortingOrder = _spriteRenderer.sortingOrder + i + 1;
            }
        }
    }
}
