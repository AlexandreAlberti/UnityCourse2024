using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance;

    [SerializeField] private RecipeListSO fullListOfRecipes;
    [SerializeField] private float spawnTimerMax;
    [SerializeField] private int waitingRecipesMax;

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    private List<RecipeSO> waitingRecipeList;
    private float spawnRecipeTimer;

    private void Awake() {
        Instance = this;
        waitingRecipeList = new List<RecipeSO>();
        spawnRecipeTimer = 0.0f;
    }

    private void Update() {
        if (waitingRecipeList.Count >= waitingRecipesMax) {
            return;
        }

        spawnRecipeTimer += Time.deltaTime;
        
        if (spawnRecipeTimer < spawnTimerMax) {
            return;
        }

        spawnRecipeTimer = 0.0f;
        RecipeSO recipe = fullListOfRecipes.recipes[UnityEngine.Random.Range(0, fullListOfRecipes.recipes.Count)];
        waitingRecipeList.Add(recipe);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for(int i = 0; i < waitingRecipeList.Count; ++i) {
            RecipeSO recipe = waitingRecipeList[i];
            bool recipeValid = true;
            if (recipe.ingredients.Count != plateKitchenObject.GetList().Count) {
                continue;
            }
            foreach(KitchenObjectSO recipeIngredient in recipe.ingredients) {
                bool ingredientMatch = false;
                foreach (KitchenObjectSO plateIngredient in plateKitchenObject.GetList()) {
                    if (recipeIngredient == plateIngredient) {
                        ingredientMatch = true;
                        break;
                    }
                }
                if (!ingredientMatch) {
                    recipeValid = false;
                    break;
                }
            }
            if (recipeValid) {
                waitingRecipeList.RemoveAt(i);
                plateKitchenObject.DestroySelf();
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                return;
            }
        }
    }

    public List<RecipeSO> GetRecipeWaitingList() {
        return waitingRecipeList;
    }
}
