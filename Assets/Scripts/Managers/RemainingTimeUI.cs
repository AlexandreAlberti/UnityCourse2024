using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainingTimeUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI remainingTimeText;

    private bool isRemainingTimeActive;

    private void Start() {
        GameManager.Instance.OnStateChanged += OnStateChanged;
        isRemainingTimeActive = false;
    }

    private void OnStateChanged(object sender, GameManager.GameState state) {
        isRemainingTimeActive = state == GameManager.GameState.Playing;
        remainingTimeText.gameObject.SetActive(isRemainingTimeActive);
    }

    private void Update() {
        if (!isRemainingTimeActive) {
            return;
        }

        int remainingTime = GameManager.Instance.GetRemainingTimeCeil();
        remainingTimeText.text = remainingTime.ToString();
    }
}