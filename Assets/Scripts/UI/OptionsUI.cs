using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {
    [SerializeField] private Scrollbar musicScrollbar;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Scrollbar sfxScrollbar;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private Button closeOptionsButton;
    [SerializeField] private GameObject optionsContainer;

    public EventHandler OnCloseMenu;

    private void Awake() {
        musicScrollbar.onValueChanged.AddListener(newValue => {
            SoundsManager.Instance.ChangeMusicVolume(newValue);
            musicText.text = Mathf.CeilToInt(newValue * 10).ToString();
        });
        sfxScrollbar.onValueChanged.AddListener(newValue => {
            SoundsManager.Instance.ChangeSFXVolume(newValue);
            sfxText.text = Mathf.CeilToInt(newValue * 10).ToString();
        });
        closeOptionsButton.onClick.AddListener(() => {
            CloseMenu();
            OnCloseMenu?.Invoke(this, EventArgs.Empty);
        });
    }

    private void Start() {
        musicScrollbar.enabled = false;
        sfxScrollbar.enabled = false;

        float sfx = PlayerPrefs.GetFloat(SoundsManager.SFX_VOLUME, 0.75f);
        sfxText.text = Mathf.CeilToInt(sfx * 10).ToString();
        sfxScrollbar.value = sfx;

        float music = PlayerPrefs.GetFloat(SoundsManager.MUSIC_VOLUME, 1.0f);
        musicText.text = Mathf.CeilToInt(music * 10).ToString();
        musicScrollbar.value = music;

        musicScrollbar.enabled = true;
        sfxScrollbar.enabled = true;
    }

    public void OpenMenu() {
        optionsContainer.SetActive(true);
        closeOptionsButton.Select();
    }

    public void CloseMenu() {
        optionsContainer.SetActive(false);
    }
}
