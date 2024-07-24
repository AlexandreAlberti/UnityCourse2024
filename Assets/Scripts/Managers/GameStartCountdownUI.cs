using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {
    private static string NUMBER_ANIMATION = "Popup";

    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private int startTextTimerMax;
    [SerializeField] private Animator animator;

    private bool isCountdownEnabled;
    private bool isShowingStartTextEnabled;
    private float startTextTimer;
    private int previousNumber;

    private void Start() {
        GameManager.Instance.OnStateChanged += OnStateChanged;
        isCountdownEnabled = false;
        countdownText.enabled = false;
        startText.enabled = false;
        previousNumber = startTextTimerMax + 1;
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

        if (remainingCountDown != previousNumber) {
            previousNumber = remainingCountDown;
            animator.SetTrigger(NUMBER_ANIMATION);
            SoundsManager.Instance.PlayCountdownSound();
        }

    }
}