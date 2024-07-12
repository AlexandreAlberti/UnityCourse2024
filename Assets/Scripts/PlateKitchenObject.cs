using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    [SerializeField] private List<KitchenObjectSO> allowedSO;
    [SerializeField] private KitchenObjectSO cookedMeatSO;
    [SerializeField] private KitchenObjectSO burnedMeatSO;

    public event EventHandler<OnPlatesIngredientsChangeEventArgs> OnPlatesIngredientsChange;
    public class OnPlatesIngredientsChangeEventArgs: EventArgs {
        public List<KitchenObjectSO> listOfIngredients;
    }


    private List<KitchenObjectSO> list;

    private void Start() {
        list = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO itemToAddToList) {
        if (!allowedSO.Contains(itemToAddToList)) {
            return false;
        }

        if (list.Contains(itemToAddToList)) {
            return false;
        }

        if (itemToAddToList == cookedMeatSO && list.Contains(burnedMeatSO)) {
            return false;
        }

        if (itemToAddToList == burnedMeatSO && list.Contains(cookedMeatSO)) {
            return false;
        }

        list.Add(itemToAddToList);

        OnPlatesIngredientsChange?.Invoke(this, new OnPlatesIngredientsChangeEventArgs { 
            listOfIngredients = list
        });
        return true;
    }
}
