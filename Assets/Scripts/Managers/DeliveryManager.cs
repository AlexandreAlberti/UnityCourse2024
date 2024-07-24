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
    public event EventHandler<SoundPositionEventArgs> OnRecipeSuccess;
    public event EventHandler<SoundPositionEventArgs> OnRecipeFailed;

    private List<RecipeSO> waitingRecipeList;
    private float spawnRecipeTimer;
    private int recipesDelivered;

    private void Awake() {
        Instance = this;
        waitingRecipeList = new List<RecipeSO>();
        spawnRecipeTimer = 0.0f;
        recipesDelivered = 0;
    }

    private void Update() {
        if (waitingRecipeList.Count >= waitingRecipesMax) {
            return;
        }

        if (!GameManager.Instance.IsGamePlaying()) {
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

    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject, Vector3 counterPosition) {
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
                OnRecipeSuccess?.Invoke(this, new SoundPositionEventArgs {
                    position = counterPosition
                });
                recipesDelivered++;
                return true;
            }
        }

        OnRecipeFailed?.Invoke(this, new SoundPositionEventArgs {
            position = counterPosition
        });

        return false;
    }

    public List<RecipeSO> GetRecipeWaitingList() {
        return waitingRecipeList;
    }

    public int GetRecipesDelivered() {
        return recipesDelivered;
    }
}
