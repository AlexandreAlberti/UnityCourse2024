using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : KitchenObjectParentAbstract {

    public static event EventHandler<SoundPositionEventArgs> OnAnyTrash;

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            // Delete Grabbed Item by player
            KitchenObject kitchenObject = player.GetKitchenObject();
            kitchenObject.DestroySelf();
            OnAnyTrash?.Invoke(this, new SoundPositionEventArgs {
                position = transform.position
            });
        }
    }
}
