using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : KitchenObjectParentAbstract {
    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            // Delete Grabbed Item by player
            KitchenObject kitchenObject = player.GetKitchenObject();
            kitchenObject.DestroySelf();
        }
    }
}
