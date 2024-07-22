using System.Collections;
using UnityEngine;

namespace MainMenu
{
    public class FaderManager : MonoBehaviour
    {
        [SerializeField] private Fader _fader;
        [SerializeField] private float _fadeInTime;
        [SerializeField] private float _fadeOutTime;
        [SerializeField] private float _loadingTime;

        public static FaderManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public IEnumerator MakeFaderAppear()
        {
            yield return _fader.Restart();
            yield return _fader.FadeOut(_fadeOutTime);
        }

        public IEnumerator MakeLoadingScreen()
        {
            yield return _fader.Restart();
            yield return _fader.DoLoading();
            yield return new WaitForSeconds(_loadingTime / 2);
        }

        public IEnumerator MakeFaderDisappear()
        {
            yield return new WaitForSeconds(_loadingTime / 2);
            yield return _fader.Restart();
            yield return _fader.FadeIn(_fadeInTime);
        }
    }
}