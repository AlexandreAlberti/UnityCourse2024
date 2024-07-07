using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
    [SerializeField] ClearCounter clearCounter;
    [SerializeField] GameObject clearCounterVisual;

    private void Start() {
        Player.Instance.OnSelectedCounterChange += CounterChanged;
    }

    private void CounterChanged(object sender, EventArgs e) {
        if (e is Player.OnSelectedCounterChangeEventArgs e2) {
            clearCounterVisual.SetActive(clearCounter == e2.selectedCounter);
        }
    }
}