using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : KitchenObjectParentAbstract {

    public override void Interact(Player player)
    {
        KitchenObject kitchenObject = GetKitchenObject();

        if (!kitchenObject && player.HasKitchenObject()) {
            // Drop to Counter
            player.GetKitchenObject().SetKitchenObjectParent(this);
        } else if (kitchenObject && !player.HasKitchenObject()) {
            // Grab by player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }
}
