using DG.Tweening;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace LevelSystem
{
    public class LevelResultsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreValueText;
        [SerializeField] private Button _resetButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private ScreenFade _screenFade;
        private LevelSwitcher _levelSwitcher;
        
        public Button.ButtonClickedEvent OnRestart => _resetButton.onClick;
        
        [Inject]
        public void Construct(ScreenFade screenFade)
        {
            _screenFade = screenFade;
        }
        
        public void ShowResults()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1,0.5f);
            gameObject.SetActive(true);
            _screenFade.DoLightFade();
        }
        
        public void HideResults()
        {
            _canvasGroup.DOFade(0,0.5f);
            gameObject.SetActive(false);
            _screenFade.DoNoFade();
        }
        
        public void SetResults(int score)
        {
            _scoreValueText.text = score.ToString();
        }
    }
}