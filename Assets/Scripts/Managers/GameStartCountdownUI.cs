using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private float startTextTimerMax;

    private bool isCountdownEnabled;
    private bool isShowingStartTextEnabled;
    private float startTextTimer;

    private void Start() {
        GameManager.Instance.OnStateChanged += OnStateChanged;
        isCountdownEnabled = false;
        countdownText.enabled = false;
        startText.enabled = false;
    }

    private void OnStateChanged(object sender, GameManager.GameState state) {
        isCountdownEnabled = state == GameManager.GameState.CountDown;
        countdownText.enabled = isCountdownEnabled;
        startTextTimer = 0.0f;
        isShowingStartTextEnabled = state == GameManager.GameState.Playing;
    }

    private void Update() {
        if (isShowingStartTextEnabled) {
            startTextTimer += Time.deltaTime;
            isShowingStartTextEnabled = startTextTimer < startTextTimerMax;
            startText.enabled = isShowingStartTextEnabled;
        }

        if (!isCountdownEnabled) {
            return;
        }
        int remainingCountDown = GameManager.Instance.GetRemainingCountdownCeil();
        countdownText.text = remainingCountDown.ToString();
    }
}