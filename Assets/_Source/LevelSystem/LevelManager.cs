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
        [SerializeField] private string _levelCompleteMessage;
        [SerializeField] private string _deathMessage;
        
        private LevelResultsView _levelResultsView;
        private IEnumerable<EnemySpawner> _spawners;
        private Player _player;
        
        [Inject]
        public void Construct(Player player, IEnumerable<EnemySpawner> spawners, LevelResultsView levelResultsView)
        {
            _player = player;
            //_player.OnDeath += OnPlayerDeath; // TODO onDeath event
            _levelResultsView = levelResultsView;
            _spawners = spawners;
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
        
        private void OnPlayerDeath()
        {
            int count = 0;
            foreach (var spawner in _spawners)
            {
                count += spawner.EntitiesKilledCount;
            }
            
            _levelResultsView.SetResults(count);
            _levelResultsView.ShowResults();
        }
        
        private async UniTaskVoid LevelNextTransition()
        {
            _levelResultsView.HideResults();
            //_player.; // TODO call player dig animation
            await UniTask.WaitForSeconds(_transitionTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        private async UniTaskVoid RestartTransition()
        {
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
