using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class StoveCounter : KitchenObjectParentAbstract {
    public enum Status {
        STOPPED, COOKING, BURNING
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipes;
    [SerializeField] private ProgressBarUI progressBarCooking;
    [SerializeField] private ProgressBarUI progressBarBurning;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public Status state;
    }

    private float cookingProgress;
    private float burningProgress;
    private Status status;
    private FryingRecipeSO recipe;

    private void Start() {
        cookingProgress = 0;
        burningProgress = 0;
        ChangeState(Status.STOPPED);
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            KitchenObject kitchenObjectOriginal = player.GetKitchenObject();
            KitchenObjectSO kitchenObjectOriginalSO = kitchenObjectOriginal.GetKitchenObjectSO();
            recipe = GetRecipeFromInput(kitchenObjectOriginalSO);
            // Drop to Stove Counter
            if (recipe) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cookingProgress = 0;
                burningProgress = 0;
                progressBarCooking.Restart();
                progressBarBurning.Restart();
                progressBarCooking.Activate();
                ChangeState(Status.COOKING);
            }
        } else if (HasKitchenObject() && !player.HasKitchenObject()) {
            // Grab by player
            GetKitchenObject().SetKitchenObjectParent(player);
            progressBarCooking.Deactivate();
            progressBarBurning.Deactivate();
            ChangeState(Status.STOPPED);
        }
    }

    private void Update() {
        if (recipe) {
            switch (status) {
                case Status.COOKING: {
                        cookingProgress += Time.deltaTime;
                        progressBarCooking.ProgressUpdate(cookingProgress / recipe.timeToCook);
                        if (cookingProgress >= recipe.timeToCook) {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(recipe.output, this);
                            progressBarCooking.Deactivate();
                            ChangeState(Status.BURNING);
                            burningProgress = 0;
                            progressBarBurning.Restart();
                            progressBarBurning.Activate();
                        }
                        break;
                    }
                case Status.BURNING: {
                        burningProgress += Time.deltaTime;
                        progressBarBurning.ProgressUpdate(burningProgress / recipe.timeToBurn);
                        if (burningProgress >= recipe.timeToBurn) {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(recipe.burned, this);
                            progressBarBurning.Deactivate();
                            ChangeState(Status.STOPPED);
                        }
                        break;
                    }
                case Status.STOPPED:
                    recipe = null;
                    break;
                default:
                    break;
            }
        }
    }

    private void ChangeState(Status newStatus) {
        status = newStatus;

        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            state = status
        });
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        return GetOutputFromRecipe(GetRecipeFromInput(input));
    }

    private static KitchenObjectSO GetOutputFromRecipe(FryingRecipeSO recipe) {
        if (recipe) {
            return recipe.output;
        }
        return null;
    }

    private static KitchenObjectSO GetBurnedFromRecipe(FryingRecipeSO recipe) {
        if (recipe) {
            return recipe.burned;
        }
        return null;
    }

    private FryingRecipeSO GetRecipeFromInput(KitchenObjectSO input) {
        foreach (FryingRecipeSO recipe in fryingRecipes) {
            if (recipe.input == input && recipe.output && recipe.burned) {
                return recipe;
            }
        }
        return null;
    }
}