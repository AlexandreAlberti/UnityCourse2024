using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOcerUI : MonoBehaviour {
    [SerializeField] private GameObject container;
    [SerializeField] private TextMeshProUGUI amountOrdersText;

    private bool isCountdownEnabled;
    private bool isShowingStartTextEnabled;
    private float startTextTimer;

    private void Start() {
        GameManager.Instance.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(object sender, GameManager.GameState state) {
        container.SetActive(state == GameManager.GameState.GameOver);
        amountOrdersText.text = DeliveryManager.Instance.GetRecipesDelivered().ToString();
    }
}