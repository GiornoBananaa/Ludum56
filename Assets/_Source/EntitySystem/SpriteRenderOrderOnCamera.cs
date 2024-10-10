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
            SetSortingOrder(_spriteRenderer);
            for (int i = 0; i < _spritesOnTop.Length; i++)
            {
                if(_spritesOnTop[i].transform.position.y >= transform.position.y)
                {
                    _spritesOnTop[i].sortingOrder = _spriteRenderer.sortingOrder + i + 1;
                }
                else
                {
                    SetSortingOrder(_spritesOnTop[i]);
                }
            }
        }
        
        private void SetSortingOrder(SpriteRenderer spriteRenderer)
        {
            Vector2 screenPosition = _camera.WorldToScreenPoint(spriteRenderer.transform.position);
            var pixelHeight = _camera.pixelHeight;
            spriteRenderer.sortingOrder = (int)Mathf.Lerp(MIN_LAYER, MAX_LAYER, (pixelHeight - screenPosition.y) / pixelHeight);
        }
    }
}
