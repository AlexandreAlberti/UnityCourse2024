using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingCounter : KitchenObjectParentAbstract {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;
    [SerializeField] private ProgressBarUI progressBar;

    public event EventHandler OnPlayerCut;

    private float cuttingProgress;

    private void Start() {
        cuttingProgress = 0;
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            // Drop to Counter
            player.GetKitchenObject().SetKitchenObjectParent(this);
            KitchenObject kitchenObjectOriginal = GetKitchenObject();
            KitchenObjectSO kitchenObjectOriginalSO = kitchenObjectOriginal.GetKitchenObjectSO();
            CuttingRecipeSO recipe = GetRecipeFromInput(kitchenObjectOriginalSO);
            if (recipe) {
                cuttingProgress = 0;
                progressBar.Restart();
            }
        } else if (HasKitchenObject() && !player.HasKitchenObject()) {
            // Grab by player
            GetKitchenObject().SetKitchenObjectParent(player);
            progressBar.Deactivate();
        }
    }
    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // Cut whatever is on the Counter
            KitchenObject kitchenObjectOriginal = GetKitchenObject();
            KitchenObjectSO kitchenObjectOriginalSO = kitchenObjectOriginal.GetKitchenObjectSO();
            CuttingRecipeSO recipe = GetRecipeFromInput(kitchenObjectOriginalSO);

            if (recipe) {
                cuttingProgress++;
                OnPlayerCut?.Invoke(this, EventArgs.Empty);
                progressBar.ProgressUpdate(cuttingProgress/recipe.necessaryCutsToPerform);

                if (cuttingProgress >= recipe.necessaryCutsToPerform) {
                    KitchenObjectSO newKitchenObject = GetOutputFromRecipe(recipe);
                    kitchenObjectOriginal.DestroySelf();
                    KitchenObject.SpawnKitchenObject(newKitchenObject, this);
                    progressBar.Deactivate();
                }
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        return GetOutputFromRecipe(GetRecipeFromInput(input));
    }

    private static KitchenObjectSO GetOutputFromRecipe(CuttingRecipeSO recipe) {
        if (recipe) {
            return recipe.output;
        }
        return null;
    }

    private CuttingRecipeSO GetRecipeFromInput(KitchenObjectSO input) {
        foreach (CuttingRecipeSO recipe in cuttingRecipes) {
            if (recipe.input == input) {
                return recipe;
            }
        }
        return null;
    }
}