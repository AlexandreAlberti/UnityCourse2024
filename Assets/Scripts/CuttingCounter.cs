using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : KitchenObjectParentAbstract {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

    public override void Interact(Player player) {
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            // Drop to Counter
            player.GetKitchenObject().SetKitchenObjectParent(this);
        } else if (HasKitchenObject() && !player.HasKitchenObject()) {
            // Grab by player
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }
    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // Cut whatever is on the Counter
            KitchenObject kitchenObjectOriginal = GetKitchenObject();
            KitchenObjectSO kitchenObjectOriginalSO = kitchenObjectOriginal.GetKitchenObjectSO();
            KitchenObjectSO newKitchenObject = GetOutputForInput(kitchenObjectOriginalSO);
            
            if (newKitchenObject != null) {
                kitchenObjectOriginal.DestroySelf();
                KitchenObject.SpawnKitchenObject(newKitchenObject, this);
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        foreach(CuttingRecipeSO recipe in cuttingRecipes) {
            if (recipe.input == input) {
                return recipe.output;
            }
        }

        return null;
    }
}