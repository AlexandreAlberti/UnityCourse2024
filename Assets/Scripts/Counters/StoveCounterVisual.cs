using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject stoveOnParticlesVisual;
    void Awake()
    {
        stoveCounter.OnStateChanged += OnStateChanged;
    }

    void OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs args) {
        switch (args.state) {
            case StoveCounter.Status.COOKING:
                stoveOnVisual.SetActive(true);
                stoveOnParticlesVisual.SetActive(true);
                break;
            case StoveCounter.Status.BURNING:
                stoveOnVisual.SetActive(true);
                stoveOnParticlesVisual.SetActive(true);
                break;
            case StoveCounter.Status.STOPPED:
            default:
                stoveOnVisual.SetActive(false);
                stoveOnParticlesVisual.SetActive(false);
                break;
        }
    }
}
