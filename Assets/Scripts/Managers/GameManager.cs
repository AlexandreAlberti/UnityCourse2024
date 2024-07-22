using MainMenu;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public enum GameState {
        WaitingToStart,
        CountDown,
        Playing,
        GameOver
    }

    [SerializeField] private float waitToStartTimerMax;
    [SerializeField] private float countdownTimerMax;
    [SerializeField] private float playTimerMax;

    public event EventHandler<GameState> OnStateChanged;

    private GameState gameState;
    private float waitToStartTimer;
    private float countdownTimer;
    private float playTimer;
    private bool paused;

    private void Awake() {
        Instance = this;
        gameState = GameState.WaitingToStart;
        waitToStartTimer = 0.0f;
        countdownTimer = countdownTimerMax;
        playTimer = playTimerMax;
        paused = false;
    }

    private void Start() {
        StartCoroutine(FaderManager.Instance.MakeFaderDisappear());
        GameInput.Instance.OnPauseAction += OnPauseAction;
    }

    public void OnPauseAction(object sender, EventArgs e) {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
    }

    private void Update() {
        if (paused) {
            return;
        }

        switch (gameState) {
            case GameState.WaitingToStart:
                waitToStartTimer += Time.deltaTime;
                if(waitToStartTimer >= waitToStartTimerMax) {
                    gameState = GameState.CountDown;
                    countdownTimer = countdownTimerMax;
                    OnStateChanged?.Invoke(this, gameState);
                }
                break;
            case GameState.CountDown:
                countdownTimer -= Time.deltaTime;
                if(countdownTimer <= 0.0f) {
                    countdownTimer = 0;
                    gameState = GameState.Playing;
                    playTimer = playTimerMax;
                    OnStateChanged?.Invoke(this, gameState);
                }
                break;
            case GameState.Playing:
                playTimer -= Time.deltaTime;
                if (playTimer <= 0.0f) {
                    gameState = GameState.GameOver;
                    OnStateChanged?.Invoke(this, gameState);
                }
                break;
            case GameState.GameOver:
            default:
                break;
        }
    }

    public bool IsGamePlaying() {
        return gameState == GameState.Playing;
    }
    public int GetRemainingCountdownCeil() {
        return Mathf.CeilToInt(countdownTimer);
    }

    public int GetRemainingTimeCeil() {
        return Mathf.CeilToInt(playTimer);
    }
}
