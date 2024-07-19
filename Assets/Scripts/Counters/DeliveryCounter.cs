using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : KitchenObjectParentAbstract {
    public override void Interact(Player player) {
        if (player.HasKitchenObject() && player.GetKitchenObject() is PlateKitchenObject plateKitchenObject) {
            // Delete Grabbed plate by player
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject, transform.position);
        }
    }
}
