using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSounds : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private AudioSource audioSource;
    void Awake()
    {
        stoveCounter.OnStateChanged += OnStateChanged;
    }

    void OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs args) {
        switch (args.state) {
            case StoveCounter.Status.BURNING:
            case StoveCounter.Status.COOKING:
                audioSource.Play();
                break;
            case StoveCounter.Status.STOPPED:
            default:
                audioSource.Pause();
                break;
        }
    }
}
