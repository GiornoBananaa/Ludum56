using System;
using System.Collections.Generic;
using AudioSystem;
using Cysharp.Threading.Tasks;
using EnemySystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace LevelSystem
{
    public class LevelSwitcher : MonoBehaviour
    {
        [field: SerializeField] public int Level { get; private set; }
        
        [SerializeField] private float _transitionTime;
        [SerializeField] private float _restartTime;
        
        private LevelResultsView _levelResultsView;
        private Player _player;
        private MusicPlayer _musicPlayer;
        private LevelCounter _levelCounter;
        
        [Inject]
        public void Construct(Player player, LevelResultsView levelResultsView, MusicPlayer musicPlayer, LevelCounter levelCounter)
        {
            _player = player;
            _player.OnDeath += OnPlayerDeath;
            _levelResultsView = levelResultsView;
            _musicPlayer = musicPlayer;
            _levelCounter = levelCounter;
        }

        private void Start()
        {
            _levelCounter.SetLevel(Level);
        }

        public void NextLevel()
        {
            LevelNextTransition();
        }
        
        public void RestartLevel()
        {
            RestartTransition();
        }

        public async void EndLevel()
        {
            await UniTask.WaitForSeconds(1);
            NextLevel();
        }
        
        private async void OnPlayerDeath()
        {
            _musicPlayer.Play(MusicType.Death);
            await UniTask.WaitForSeconds(2);
            
            _levelResultsView.OnRestart.AddListener(RestartLevel);
            _levelResultsView.SetResults(_player.killedEnemies);
            _levelResultsView.ShowResults();
        }
        
        private async UniTaskVoid LevelNextTransition()
        {
            _player.OnLevelSwitch?.Invoke();
            await UniTask.WaitForSeconds(_transitionTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        private async UniTaskVoid RestartTransition()
        {
            _levelResultsView.HideResults();
            await UniTask.WaitForSeconds(_restartTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
