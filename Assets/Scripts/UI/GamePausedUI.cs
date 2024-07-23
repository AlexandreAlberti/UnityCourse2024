using MainMenu;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private OptionsUI _optionsMenu;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _optionsButton;

    private bool paused;

    private void Start() {
        paused = false;
        GameInput.Instance.OnPauseAction += OnPauseAction;
        _optionsMenu.OnCloseMenu += OnClosedOptionsAction;

        _mainMenuButton.onClick.AddListener(() => {
            GameManager.Instance.OnPauseAction(this, EventArgs.Empty);
            GameInput.Instance.OnPauseAction -= OnPauseAction;
            SceneManager.LoadScene(0);
        });
        _resumeButton.onClick.AddListener(() => {
            GameManager.Instance.OnPauseAction(this, EventArgs.Empty);
            OnPauseAction(this, EventArgs.Empty);
        });

        _pauseMenu.SetActive(paused);

        _optionsButton.onClick.AddListener(() => {
            _pauseMenu.SetActive(false);
            _optionsMenu.OpenMenu();
        });
    }

    private void OnClosedOptionsAction(object sender, EventArgs e) {
        _pauseMenu.SetActive(true);
        _optionsButton.Select();
    }

    private void OnPauseAction(object sender, EventArgs e) {
        paused = !paused;
        _pauseMenu.SetActive(paused);
        if (!paused) {
            _optionsMenu.CloseMenu();
        } else {
            _optionsButton.Select();
        }
    }
}
