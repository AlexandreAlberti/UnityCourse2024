using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : KitchenObjectParentAbstract {

    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && player.HasKitchenObject()) {
            // Drop to Counter
            player.GetKitchenObject().SetKitchenObjectParent(this);
        } else if (HasKitchenObject() && !player.HasKitchenObject()) {
            // Grab by player
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }
}
