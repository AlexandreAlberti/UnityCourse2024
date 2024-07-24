using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image sprite;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failureSprite;
    [SerializeField] private string successText;
    [SerializeField] private string failureText;
    [SerializeField] private float messageShowTime;

    private bool enabled;
    private float messageShowTimer;

    private void Start() {
        background.gameObject.SetActive(false);
        enabled = false;
    }

    private void Update() {
        if (!enabled) { return; }

        messageShowTimer += Time.deltaTime;
        if (messageShowTimer > messageShowTime) {
            background.gameObject.SetActive(false);
            enabled = false;
        }
    }

    public void ShowSuccess() {
        background.gameObject.SetActive(true);
        messageShowTimer = 0.0f;
        enabled = true;

        background.color = successColor;
        sprite.sprite = successSprite;
        resultText.text = successText;
    }

    public void ShowFailure() {
        background.gameObject.SetActive(true);
        messageShowTimer = 0.0f;
        enabled = true;

        background.color = failureColor;
        sprite.sprite = failureSprite;
        resultText.text = failureText;
    }
}
