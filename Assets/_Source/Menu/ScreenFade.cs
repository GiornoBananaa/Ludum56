using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class ScreenFade: MonoBehaviour
    {
        [SerializeField] private Image _fadeImage;
        [SerializeField] private float _fadeTime;
        
        private Tween _tween;
        
        public Tween DoMaxFade()
        {
            _tween?.Kill();
            _tween = _fadeImage.DOFade(1f, _fadeTime);
            return _tween;
        }
            
        public Tween DoLightFade()
        {
            _tween?.Kill();
            _tween = _fadeImage.DOFade(0.8f,_fadeTime);
            return _tween;
        }
        
        public Tween DoNoFade()
        {
            _tween?.Kill();
            _tween = _fadeImage.DOFade(0f, _fadeTime);
            return _tween;
        }
    }
}