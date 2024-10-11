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
    private const string ATTACK_LEVEL_PROPERTY = "Attack";
    private const string SKILL_LEVEL_PROPERTY = "Skill";
    private const string AUTOATTACK_LEVEL_PROPERTY = "AutoAttack";
    
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

        ResetUpgrades();
    }

    private void ResetUpgrades()
    {
        PlayerPrefs.SetInt(ATTACK_LEVEL_PROPERTY, 1);
        PlayerPrefs.SetInt(SKILL_LEVEL_PROPERTY, 1);
        PlayerPrefs.SetInt(AUTOATTACK_LEVEL_PROPERTY, 0);
        PlayerPrefs.Save();
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
