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
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    private bool paused;

    private void Start() {
        paused = false;
        GameInput.Instance.OnPauseAction += OnPauseAction;

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
    }

    private void OnPauseAction(object sender, EventArgs e) {
        paused = !paused;
        _pauseMenu.SetActive(paused);
    }
}
