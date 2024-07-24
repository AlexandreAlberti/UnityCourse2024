using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : KitchenObjectParentAbstract {

    [SerializeField] DeliveryResultUI resultPanel;
    public override void Interact(Player player) {
        if (player.HasKitchenObject() && player.GetKitchenObject() is PlateKitchenObject plateKitchenObject) {
            // Delete Grabbed plate by player
            if (DeliveryManager.Instance.DeliverRecipe(plateKitchenObject, transform.position)) {
                resultPanel.ShowSuccess();
            } else {
                resultPanel.ShowFailure();
            }
        }
    }
}
