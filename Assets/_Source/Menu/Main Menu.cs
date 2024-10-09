using System;
using System.Collections;
using System.Collections.Generic;
using LevelSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

public class MainMenu : MonoBehaviour
{
    private const int ARCADE_SCENE_INDEX = 4;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _arcadeButton;
    [SerializeField] private Image _lockArcadeButton;

    private LevelCounter _levelCounter;
    
    [Inject]
    public void Construct(LevelCounter levelCounter)
    {
        _levelCounter = levelCounter;
    }

    private void Start()
    {
        _startGameButton.onClick.AddListener(PlayGame);
        
        if(_levelCounter.GetLevel() >= 4)
        {
            _arcadeButton.interactable = true;
            _arcadeButton.onClick.AddListener(PlayArcade);
            _lockArcadeButton.enabled = false;
        }
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void PlayArcade()
    {
        SceneManager.LoadScene(ARCADE_SCENE_INDEX);
    }
}
