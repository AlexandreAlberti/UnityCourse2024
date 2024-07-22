using System.Collections;
using UnityEngine;

namespace MainMenu
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _loadingObject;

        public void FadeOutImmediate()
        {
            _canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator DoLoading()
        {
            _loadingObject.SetActive(true);
            yield return null;
        }

        public IEnumerator Restart()
        {
            _loadingObject.SetActive(false);
            yield return null;
        }

        public IEnumerator FadeIn(float time)
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}