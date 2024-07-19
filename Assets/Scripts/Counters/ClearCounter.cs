using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : KitchenObjectParentAbstract {

    public static event EventHandler<SoundPositionEventArgs> OnPlayerDrop;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            // Drop to Counter
            player.GetKitchenObject().SetKitchenObjectParent(this);
        } else if (HasKitchenObject() && !player.HasKitchenObject()) {
            // Grab by player
            GetKitchenObject().SetKitchenObjectParent(player);
        } else if (HasKitchenObject() && player.HasKitchenObject() && player.GetKitchenObject() is PlateKitchenObject plateKitchenObject) {
            // Grab by player with plate if we can
            if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                GetKitchenObject().DestroySelf();
            }
        } else if (HasKitchenObject() && player.HasKitchenObject() && GetKitchenObject() is PlateKitchenObject plateKitchenObject2) {
            // Drop by player in a plate if we can
            if (plateKitchenObject2.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }

    public override void SetKichenObject(KitchenObject kitchenObject) {
        base.SetKichenObject(kitchenObject);
        if (kitchenObject != null) {
            OnPlayerDrop?.Invoke(this, new SoundPositionEventArgs {
                position = transform.position
            });
        }
    }
}
