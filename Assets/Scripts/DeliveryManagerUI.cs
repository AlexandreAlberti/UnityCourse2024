using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {
    [SerializeField] private Transform container;
    [SerializeField] private Transform template;

    private void Awake() {
        template.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeCompleted += OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += OnRecipeSpawned;

        UpdateVisual();
    }

    private void OnRecipeSpawned(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void OnRecipeCompleted(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child == template) { continue; }
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipe in DeliveryManager.Instance.GetRecipeWaitingList()) {
            Transform recipeTransform = Instantiate(template, container);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipe);
            recipeTransform.gameObject.SetActive(true);
        }
    }
}
