using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EnemySystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        private const string LEVEL_PROPERTY = "Level";
        [field: SerializeField] public int Level { get; private set; }
        
        [SerializeField] private float _transitionTime;
        [SerializeField] private float _restartTime;
        
        private LevelResultsView _levelResultsView;
        private Player _player;
        
        [Inject]
        public void Construct(Player player, LevelResultsView levelResultsView)
        {
            _player = player;
            _player.OnDeath += OnPlayerDeath;
            _levelResultsView = levelResultsView;
        }
        
        private void Awake()
        {
            SaveData();
        }
        
        public void NextLevel()
        {
            LevelNextTransition();
        }
        
        public void RestartLevel()
        {
            RestartTransition();
        }
        
        public void ResetLevelData()
        {
            PlayerPrefs.SetInt(LEVEL_PROPERTY, 0);
            PlayerPrefs.Save();
        }

        public async void EndLevel()
        {
            await UniTask.WaitForSeconds(1);
            NextLevel();
        }
        
        private async void OnPlayerDeath()
        {
            await UniTask.WaitForSeconds(2);
            
            _levelResultsView.OnRestart.AddListener(RestartLevel);
            //_levelResultsView.SetResults(_spawner.EntitiesKilledCount);
            _levelResultsView.ShowResults();
        }
        
        private async UniTaskVoid LevelNextTransition()
        {
            //_player.; // TODO call player dig animation
            await UniTask.WaitForSeconds(_transitionTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        private async UniTaskVoid RestartTransition()
        {
            _levelResultsView.HideResults();
            await UniTask.WaitForSeconds(_restartTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void SaveData()
        {
            int level = PlayerPrefs.GetInt(LEVEL_PROPERTY, 0);
             PlayerPrefs.SetInt(LEVEL_PROPERTY, PlayerPrefs.GetInt(LEVEL_PROPERTY, level > Level ? level : Level));
             PlayerPrefs.Save();
        }
    }
}
