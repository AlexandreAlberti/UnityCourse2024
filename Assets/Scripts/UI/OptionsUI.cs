using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Scrollbar musicScrollbar;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Scrollbar sfxScrollbar;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private Button closeOptionsButton;
    [SerializeField] private GameObject optionsContainer;

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
               optionsContainer.SetActive(false);
        });
        musicScrollbar.enabled = false;
        sfxScrollbar.enabled = false;
    }

    private void Start() {
        musicScrollbar.enabled = false;
        sfxScrollbar.enabled = false;


        if (PlayerPrefs.HasKey(SoundsManager.SFX_VOLUME)) {
            float value = PlayerPrefs.GetFloat(SoundsManager.SFX_VOLUME);
            sfxText.text = Mathf.CeilToInt(value * 10).ToString();
            sfxScrollbar.value = value;
        } else {
            sfxText.text = "5";
            sfxScrollbar.value = 0.5f;
        }


        if (PlayerPrefs.HasKey(SoundsManager.MUSIC_VOLUME)) {
            float value = PlayerPrefs.GetFloat(SoundsManager.MUSIC_VOLUME);
            musicText.text = Mathf.CeilToInt(value * 10).ToString();
            musicScrollbar.value = value;
        } else {
            musicText.text = "10";
            musicScrollbar.value = 1;
        }

        musicScrollbar.enabled = true;
        sfxScrollbar.enabled = true;
    }
}
