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
        
        public void DoMaxFade()
        {
            _tween?.Kill();
            _tween = _fadeImage.DOFade(1f, _fadeTime);
        }
            
        public void DoLightFade()
        {
            _tween?.Kill();
            _tween = _fadeImage.DOFade(0.8f,_fadeTime);
        }
        
        public void DoNoFade()
        {
            _tween?.Kill();
            _tween = _fadeImage.DOFade(0f, _fadeTime);
        }
    }
}