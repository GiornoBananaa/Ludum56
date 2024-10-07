using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace LevelSystem
{
    public class LevelResultsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreValueText;
        [SerializeField] private Button _button;

        private ScreenFade _screenFade;
        private LevelManager _levelManager;
        
        [Inject]
        public void Construct(ScreenFade screenFade, LevelManager levelManager)
        {
            _screenFade = screenFade;
            _levelManager = levelManager;
            _button.onClick.AddListener(ResetButton);
        }
        
        private void ResetButton()
        {
            _levelManager.RestartLevel();
        }
        
        public void ShowResults()
        {
            _screenFade.DoLightFade();
        }
        
        public void HideResults()
        {
            _screenFade.DoNoFade();
        }
        
        public void SetResults(int score)
        {
            _scoreValueText.text = score.ToString();
        }
    }
}