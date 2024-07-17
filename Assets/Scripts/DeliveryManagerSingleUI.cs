using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
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

    public void SetRecipeSO (RecipeSO recipe) {
        title.text = recipe.recipeName;
        bread.SetActive(recipe.ingredients.Contains(breadSO));
        cutTomato.SetActive(recipe.ingredients.Contains(cutTomatoSO));
        cutLetucce.SetActive(recipe.ingredients.Contains(cutLetucceSO));
        cutCheese.SetActive(recipe.ingredients.Contains(cutCheeseSO));
        cookedMeat.SetActive(recipe.ingredients.Contains(cookedMeatSO));
        burnedMeat.SetActive(recipe.ingredients.Contains(burnedMeatSO));
    }
}
