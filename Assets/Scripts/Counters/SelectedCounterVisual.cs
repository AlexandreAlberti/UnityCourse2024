using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
    [SerializeField] KitchenObjectParentAbstract selectedKitchenParent;
    [SerializeField] GameObject[] kitchenParentVisuals;

    private void Start() {
        Player.Instance.OnSelectedCounterChange += CounterChanged;
    }

    private void CounterChanged(object sender, EventArgs e) {
        if (e is Player.OnSelectedCounterChangeEventArgs e2) {
            bool isActive = selectedKitchenParent == e2.selectedKitchenParent;
            foreach (GameObject kitchenParentVisual in kitchenParentVisuals) {
                kitchenParentVisual.SetActive(isActive);
            }
        }
    }
}