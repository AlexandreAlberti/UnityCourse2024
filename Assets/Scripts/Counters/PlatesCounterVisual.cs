using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private GameObject[] pileOfPlatesOrdered;

    private void Start() {
        platesCounter.OnPlatesAmountChange += PlateAmountChange;
    }

    public void PlateAmountChange(object sender, int amount) {
        if (pileOfPlatesOrdered.Length >= 1) {
            pileOfPlatesOrdered[0].SetActive(amount >= 1);
        }
        if (pileOfPlatesOrdered.Length >= 2) {
            pileOfPlatesOrdered[1].SetActive(amount >= 2);
        }
        if (pileOfPlatesOrdered.Length >= 3) {
            pileOfPlatesOrdered[2].SetActive(amount >= 3);
        }
    }
}
