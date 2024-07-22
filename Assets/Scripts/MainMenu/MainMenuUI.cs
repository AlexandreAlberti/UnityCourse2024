using MainMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            // PlayGame code
            StartCoroutine(LoadGameScene());
        });

        quitButton.onClick.AddListener(() => {
            // Quit code
            Application.Quit();
        });
    }

    private void Start() {
        StartCoroutine(FaderManager.Instance.MakeFaderDisappear());
    }

    public IEnumerator LoadGameScene() {
        yield return FaderManager.Instance.MakeFaderAppear();
        yield return FaderManager.Instance.MakeLoadingScreen();
        yield return SceneManager.LoadSceneAsync(1);
    }
}
