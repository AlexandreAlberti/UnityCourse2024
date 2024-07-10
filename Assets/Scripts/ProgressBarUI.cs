using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Image progressBarImage;
    
    private void Start() {
        progressBarImage.fillAmount = 0;
        progressBar.SetActive(false);
    }

    public void Restart() {
        progressBarImage.fillAmount = 0;
        progressBar.SetActive(true);
    }

    public void ProgressUpdate(float progress) {
        progressBarImage.fillAmount = progress;
    }

    public void Deactivate() {
        progressBar.SetActive(false);
    }
}
