﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class UISwitcher : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private GameObject _menu;

    private void OnEnable()
    {
        _game.LevelFinished += OnLevelFinish;
        _game.PlayerDied += OnPlayerDead;
        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        _newGameButton.onClick.AddListener(OnNewGameButtonClick);
        _menuButton.onClick.AddListener(OnMenuButtonClick);
    }

    private void OnDisable()
    {
        _game.LevelFinished -= OnLevelFinish;
        _game.PlayerDied -= OnPlayerDead;
        _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClick);
        _newGameButton.onClick.RemoveListener(OnNewGameButtonClick);
        _menuButton.onClick.RemoveListener(OnMenuButtonClick);
    }

    private void OnPlayerDead()
    {
        _losePanel.SetActive(true);
    }

    private void OnMenuButtonClick()
    {
        _menu.SetActive(true);
    }

    private void OnNextLevelButtonClick()
    {
        if (_game.HasNextLevel()) 
        {
            _game.StartNextLevel();
            _winPanel.SetActive(false);
        }
    }

    private void OnNewGameButtonClick()
    {
        _game.StartNewGame();
        _losePanel.SetActive(false);
    }

    private void OnLevelFinish()
    {
        _winPanel.SetActive(true);
    }
}
