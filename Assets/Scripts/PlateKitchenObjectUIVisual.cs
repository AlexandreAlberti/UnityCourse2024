using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObjectUIVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject platesCounter;
    [SerializeField] private GameObject bread;
    [SerializeField] private KitchenObjectSO breadSO;
    [SerializeField] private GameObject cutTomato;
    [SerializeField] private KitchenObjectSO cutTomatoSO;
    [SerializeField] private GameObject cutLetucce;
    [SerializeField] private KitchenObjectSO cutLetucceSO;
    [SerializeField] private GameObject cutCheese;
    [SerializeField] private KitchenObjectSO cutCheeseSO;
    [SerializeField] private GameObject cookedMeat;
    [SerializeField] private KitchenObjectSO cookedMeatSO;
    [SerializeField] private GameObject burnedMeat;
    [SerializeField] private KitchenObjectSO burnedMeatSO;

    private void Start() {
        platesCounter.OnPlatesIngredientsChange += PlatesIngredientsChange;
    }

    public void PlatesIngredientsChange(object sender, PlateKitchenObject.OnPlatesIngredientsChangeEventArgs args) {
        bread.SetActive(args.listOfIngredients.Contains(breadSO));
        cutTomato.SetActive(args.listOfIngredients.Contains(cutTomatoSO));
        cutLetucce.SetActive(args.listOfIngredients.Contains(cutLetucceSO));
        cutCheese.SetActive(args.listOfIngredients.Contains(cutCheeseSO));
        cookedMeat.SetActive(args.listOfIngredients.Contains(cookedMeatSO));
        burnedMeat.SetActive(args.listOfIngredients.Contains(burnedMeatSO));
    }
}
