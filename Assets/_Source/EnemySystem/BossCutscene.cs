using Cysharp.Threading.Tasks;
using DG.Tweening;
using LevelSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer;
using Random = UnityEngine.Random;

namespace EnemySystem
{
    public class BossCutscene : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _sprite1;
        [SerializeField] private Sprite _sprite2;
        private ScreenFade _screenFade;
        
        public UnityEvent OnCutsceneEnd;
        
        [Inject]
        public void Construct(ScreenFade screenFade)
        {
            _screenFade = screenFade;
        }
        
        public void LaunchCutscene()
        {
            Cutscene();
        }

        private async UniTask Cutscene()
        {
            _image.gameObject.SetActive(true);
            _screenFade.DoLightFade();
            await UniTask.WaitForSeconds(1);
            _image.DOFade(1,1);
            _image.sprite = _sprite1;
            await UniTask.WaitForSeconds(1);
            _image.rectTransform.DOAnchorPos( new Vector2(Random.Range(35,50), 0), Random.Range(0.05f, 0.1f))
                .OnComplete(() => _image.rectTransform.DOAnchorPos(new Vector2(Random.Range(-50,-35), 0), Random.Range(0.05f, 0.1f)))
                .SetLoops(16, LoopType.Yoyo);
            await UniTask.WaitForSeconds(1);
            _image.sprite = _sprite2;
            await UniTask.WaitForSeconds(2);
            _image.DOFade(0,1);
            _screenFade.DoNoFade();
            await UniTask.WaitForSeconds(1);
            _image.gameObject.SetActive(false);
            OnCutsceneEnd?.Invoke();
        }
    }
}